﻿using System;
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
    /// @des : 拆分cache部分 方便cache优先
    /// </summary>
    public class ConfigDeal : IPrivateMsgDeal
    {

        private const string AddFlag = nameof(AddFlag);
        private const string RemoveFlag = nameof(RemoveFlag);

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