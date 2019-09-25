using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Services.PikachuSystem;
using StackExchange.Redis;

namespace GenerateMsg.PrivateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 10:35:17
    /// @source : 
    /// @des : 
    /// </summary>
    public class ConfigCacheDeal : IPrivateMsgDeal
    {


        private const string AddFlag = nameof(AddFlag);
        private const string RemoveFlag = nameof(RemoveFlag);
        private readonly static TimeSpan Expiry = TimeSpan.FromSeconds(30);

        private IDatabase _database;

        public ConfigCacheDeal(IDatabase database, ConfigService configService)
        {
            _database = database;
            ConfigService = configService;
        }

        private ConfigService ConfigService { get; }

        public string Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            var key = $"{nameof(ConfigDeal)}_{context.FromQq}";

            Match match;
            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*配置管理[\s|\n|\r]*$"))
            {
                return @"当前配置管理支持内容:
[查看配置] [添加配置] [删除配置]
";
            }

            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*查看配置[\s|\n|\r]*$"))
            {
                var list = ConfigService.GetAll().OrderByDescending(u => u.UpdateTime)
                    .ThenByDescending(u => u.CreateTime).ToList();

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

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*添加配置([\s|\S]*)$")).Success)
            {
                var info = match.Groups[1].Value;
                if (string.IsNullOrWhiteSpace(info))
                {
                    // 添加标记
                    if (_database.StringSet(key, AddFlag, Expiry))
                    {
                        return "请按照此格式填写你要添加的配置:[配置key]|[配置value]|[配置描述](请注意内容中不要使用'|')";
                    }

                    return "缓存key失败！";
                }
                ConfigService.AddInfo(info, out var msg);
                return msg;
            }

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*删除配置([\s|\S]*)$")).Success)
            {
                var info = match.Groups[1].Value;
                if (string.IsNullOrWhiteSpace(info))
                {
                    // 添加标记
                    if (_database.StringSet(key, RemoveFlag, Expiry))
                    {
                        return "请输入你要删除的'配置key':";
                    }

                    return "缓存key失败！";
                }

                ConfigService.RemoveKey(info.Trim(),out var msg);
                return msg;
            }

            return String.Empty;
        }

    }
}