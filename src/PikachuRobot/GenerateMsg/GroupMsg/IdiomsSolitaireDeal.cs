using Data.Utils;
using GenerateMsg.CusConst;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Services.Utils;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 16:20:57
    /// @source : 
    /// @des : 成语接龙
    /// </summary>
    public class IdiomsSolitaireDeal : IGroupMsgDeal
    {
        private readonly IDatabase database;
        private Random random = new Random();

        public IdiomsSolitaireDeal(IdiomsService idiomsService, IDatabase database)
        {
            IdiomsService = idiomsService;
            this.database = database;
        }

        public IdiomsService IdiomsService { get; }

        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {
            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*成语接龙[\s|\n|\r]*$"))
            {


                var count = IdiomsService.GetCount();

                var randIndex = random.Next(count);

                var info = IdiomsService.GetInfo(randIndex + 1);

                // 写入活动缓存
                database.StringSet(CacheConst.GetGroupActivityKey(context.FromGroup), CacheConst.IdiomsSolitaire , CacheConst.GroupActivityExpiry);

                // 缓存尾拼
                database.StringSet(CacheConst.GetIdiomsKey(context.FromGroup), info.LastSpell, CacheConst.GroupActivityExpiry);

                // 缓存尝试次数
                database.StringSet(CacheConst.GetIdiomsTryCountKey(context.FromGroup), 0, CacheConst.GroupActivityExpiry);

                return $@"
>>>>>>>>>全员成语接龙已开启<<<<<<<<<<<<
    当前成语:{info.Word}
    尾拼:{info.LastSpell}
    成语解析:{info.Explanation}
";

            }

            return string.Empty;
        }
    }
}
