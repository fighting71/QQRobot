using Data.Pikachu;
using Data.Utils;
using GenerateMsg.CusConst;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Services.PikachuSystem;
using Services.Utils;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 16:59:15
    /// @source : 
    /// @des : 
    /// </summary>
    public class IdiomsSolitaireCacheDeal : IGroupMsgDeal
    {
        private readonly IDatabase database;
        private Random random = new Random();

        public IdiomsSolitaireCacheDeal(IdiomsService idiomsService, IDatabase database, BillFlowService billFlowService, MemberInfoService memberInfoService)
        {
            IdiomsService = idiomsService;
            this.database = database;
            BillFlowService = billFlowService;
            MemberInfoService = memberInfoService;
        }

        public IdiomsService IdiomsService { get; }
        public BillFlowService BillFlowService { get; }
        public MemberInfoService MemberInfoService { get; }

        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            var groupActivity = database.StringGet(CacheConst.GetGroupActivityKey(context.FromGroup));

            if (CacheConst.IdiomsSolitaire.Equals(groupActivity))
            {

                // 增加尝试次数
                database.StringIncrement(CacheConst.GetIdiomsTryCountKey(context.FromGroup));

                var word = context.Message.Trim();

                if (word.Length != 4)
                {
                    return "输入格式有误！";
                }

                var info = IdiomsService.GetInfo(word);

                if(info == null)
                {
                    return "词语不存在！";
                }

                var spell = database.StringGet(CacheConst.GetIdiomsKey(context.FromGroup));

                if (!spell.Equals(info.FirstSpell))
                {
                    return "你输入的词语并不能接上呢";
                }

                // 积分奖励
                var amount = random.Next(100) + 5;

                BillFlowService.AddBill(context.FromGroup, context.FromQq, amount, amount, Data.Pikachu.Menu.BillTypes.Reward, "成语接龙奖励");

                MemberInfoService.ChangeAmount(context.FromGroup, context.FromQq, amount);

                // 重新缓存
                database.StringSet(CacheConst.GetIdiomsKey(context.FromGroup), info.LastSpell, CacheConst.GroupActivityExpiry);

                // 重新缓存
                database.StringSet(CacheConst.GetIdiomsTryCountKey(context.FromGroup), 0, CacheConst.GroupActivityExpiry);

                return $@"恭喜你获得{amount.ToString()}钻石奖励

>>>>>>>>>next<<<<<<<<<<<<
    当前成语:{info.Word}
    尾拼:{info.LastSpell}
    成语解析:{info.Explanation}
";

            }

            return string.Empty;
        }
    }
}
