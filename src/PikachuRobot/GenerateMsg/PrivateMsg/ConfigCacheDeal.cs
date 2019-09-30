using System;
using System.Text;
using System.Threading.Tasks;
using GenerateMsg.CusConst;
using IServiceSupply;
using Services.PikachuSystem;
using StackExchange.Redis;

namespace GenerateMsg.PrivateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 10:35:17
    /// @source : 
    /// @des : 拆分cache部分 方便cache优先
    /// </summary>
    public class ConfigCacheDeal : IGeneratePrivateMsgDeal
    {
        private readonly IDatabase _database;

        private ConfigService ConfigService { get; }

        public ConfigCacheDeal(IDatabase database, ConfigService configService)
        {
            _database = database;
            ConfigService = configService;
        }

        public async Task<PrivateRes> Run(string msg, string account, Lazy<string> getLoginAccount)
        {
            var key = CacheConst.GetConfigKey(account);

            // 查看是否存在操作

            var cache = _database.StringGet(key).ToString();

            if (!string.IsNullOrWhiteSpace(cache))
            {
                _database.KeyDelete(key); // 移除key
                if (CacheConst.AddFlag.Equals(cache))
                {
                    return await AddInfo(msg);
                }

                if (CacheConst.RemoveFlag.Equals(cache))
                {
                    await ConfigService.RemoveKeyAsync(msg);
                    return "删除成功!";
                }
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

            var builder = new StringBuilder();
            builder.AppendLine("添加配置成功!");
            builder.AppendLine();
            builder.AppendLine("添加配置:[配置key]|[配置value]|[配置描述](请注意内容中不要使用'|')");
            builder.AppendLine("示例: 添加配置 monster|怪兽|翻译测试  ");

            return builder.ToString();
        }
    }
}