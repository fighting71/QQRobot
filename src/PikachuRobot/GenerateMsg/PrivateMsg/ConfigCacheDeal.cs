using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Pikachu;
using Data.Pikachu.Models;
using GenerateMsg.Services;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using StackExchange.Redis;

namespace GenerateMsg.PrivateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 10:35:17
    /// @source : 
    /// @des : 拆分cache部分 方便cache优先
    /// </summary>
    public class ConfigDeal : IPrivateMsgDeal
    {
        private const string AddFlag = "add";
        private const string RemoveFlag = "remove";

        /// <summary>
        /// 有效期
        /// </summary>
        private static TimeSpan Expiry = TimeSpan.FromSeconds(30);

        private IDatabase _database;

        public ConfigService ConfigService { get; }

        public ConfigDeal(IDatabase database, ConfigService configService)
        {
            _database = database;
            ConfigService = configService;
        }

        public string Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            var key = $"{nameof(ConfigDeal)}_{context.FromQq}";

            // 查看是否存在操作

            var cache = _database.StringGet(key).ToString();

            if (!string.IsNullOrWhiteSpace(cache))
            {
                _database.KeyDelete(key); // 移除key
                if (AddFlag.Equals(cache))
                {
                    ConfigService.AddInfo(context.Message,out var msg);
                    return msg;
                }

                if (RemoveFlag.Equals(cache))
                {
                    ConfigService.RemoveKey(context.Message.Trim(),out var msg);
                    return msg;
                }
            }

            return String.Empty;
        }

    }
}