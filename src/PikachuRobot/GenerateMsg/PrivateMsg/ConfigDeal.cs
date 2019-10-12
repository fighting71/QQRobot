using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Command.CusConst;
using IServiceSupply;
using Services.PikachuSystem;
using StackExchange.Redis;

namespace GenerateMsg.PrivateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 10:35:17
    /// @source : 
    /// @des : 配置设置
    /// </summary>
    public class ConfigDeal : IGeneratePrivateMsgDeal
    {
        private readonly IDatabase _database;

        public ConfigDeal(IDatabase database, ConfigService configService)
        {
            _database = database;
            ConfigService = configService;
        }

        private ConfigService ConfigService { get; }

        public async Task<PrivateRes> Run(string msg, string account, Lazy<string> getLoginAccount)
        {
            var key = CacheConst.GetConfigKey(account);

            if ("配置管理".Equals(msg))
            {
                return @"当前配置管理支持内容:
[查看配置] [添加配置] [删除配置]
";
            }

            if ("查看配置".Equals(msg))
            {
                var list = await ConfigService.GetAll().OrderByDescending(u => u.UpdateTime)
                    .ThenByDescending(u => u.CreateTime).ToListAsync();

                if (list.Count == 0) return "暂无配置";

                StringBuilder builder = new StringBuilder();

                builder.AppendLine($"  [配置key]|[配置value]|[配置描述]");

                for (int i = 0; i < list.Count(); i++)
                {
                    builder.AppendLine(
                        $"{(i + 1).ToString()}. {list[i].Key}|{list[i].Value}|{list[i].Description}");
                }

                builder.AppendLine();
                builder.AppendLine("添加配置:[配置key]|[配置value]|[配置描述](请注意内容中不要使用'|')");
                builder.AppendLine("示例: 添加配置 monster|怪兽|翻译测试  ");

                return builder.ToString();
            }

            Match match;

            if ((match = Regex.Match(msg, @"^添加配置([\s|\S]*)$")).Success)
            {
                var info = match.Groups[1].Value;
                if (string.IsNullOrWhiteSpace(info))
                {
                    // 添加标记
                    _database.StringSet(key, CacheConst.AddFlag, RuleConst.PrivateOptExpiry);

                    return "请按照此格式填写你要添加的配置:[配置key]|[配置value]|[配置描述](请注意内容中不要使用'|')";
                }

                return await AddInfo(info);
            }

            if ((match = Regex.Match(msg, @"^删除配置([\s|\S]*)$")).Success)
            {
                var info = match.Groups[1].Value;
                if (string.IsNullOrWhiteSpace(info))
                {
                    // 添加标记
                    _database.StringSet(key, CacheConst.RemoveFlag, RuleConst.PrivateOptExpiry);
                    
                    return "请输入你要删除的'配置key':";
                }

                await ConfigService.RemoveKeyAsync(info.Trim());
                return "删除成功！";
            }

            return null;
        }

        private async Task<string> AddInfo(string msg)
        {
            var info = msg.Split('|');

            if (info.Length != 3)
                return "   输入格式有误！";

            if (string.IsNullOrWhiteSpace(info[0]))
                return "   配置key不能为空！";

            await ConfigService.AddInfoAsync(info[0].Trim(), info[1], info[2]);

            return "添加成功!";
        }
    }
}