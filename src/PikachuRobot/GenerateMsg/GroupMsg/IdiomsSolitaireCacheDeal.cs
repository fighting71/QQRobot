﻿using Data.Pikachu;
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

        public GroupRes Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            var activityKey = CacheConst.GetGroupActivityKey(context.FromGroup);

            var groupActivity = database.StringGet(activityKey);

            if (CacheConst.IdiomsSolitaire.Equals(groupActivity))
            {

                // 增加尝试次数
                var tryCount = database.StringIncrement(CacheConst.GetIdiomsTryCountKey(context.FromGroup));

                var word = context.Message.Trim();

                if (word.Length != 4)
                {
                    return GroupRes.GetSuccess(new GroupItemRes() { AtTa = true, Msg = "输入格式有误！" }, GetTryCountRes(tryCount, activityKey));
                }

                var info = IdiomsService.GetInfo(word);

                if(info == null)
                {
                    return GroupRes.GetSuccess(new GroupItemRes() { AtTa = true, Msg = "词语输入有误！" }, GetTryCountRes(tryCount, activityKey));
                }

                var spell = database.StringGet(CacheConst.GetIdiomsKey(context.FromGroup));

                if (!spell.Equals(info.FirstSpell))
                {
                    return GroupRes.GetSuccess(new GroupItemRes() { AtTa = true, Msg = "你输入的词语并不能接上呢！" }, GetTryCountRes(tryCount, activityKey));
                }

                // 积分奖励
                var amount = random.Next(100) + 5;

                BillFlowService.AddBill(context.FromGroup, context.FromQq, amount, amount, Data.Pikachu.Menu.BillTypes.Reward, "成语接龙奖励");

                MemberInfoService.ChangeAmount(context.FromGroup, context.FromQq, amount);

                // 重新缓存
                database.StringSet(CacheConst.GetIdiomsKey(context.FromGroup), info.LastSpell, CacheConst.GroupActivityExpiry);

                // 重新缓存
                database.StringSet(CacheConst.GetIdiomsTryCountKey(context.FromGroup), 0, CacheConst.GroupActivityExpiry);


                return GroupRes.GetSuccess(new GroupItemRes() { AtTa = true, Msg = $"恭喜你获得{ amount.ToString() }钻石奖励！" },
                    @"
>>>>>>>>>【成语接龙】下一回合<<<<<<<<<<<<
    当前成语:{info.Word}
    尾拼:{info.LastSpell}
    成语解析:{info.Explanation}
"
                    );

            }

            return null;
        }

        public string GetTryCountRes(long tryCount,string activityKey)
        {

            if(tryCount > RuleConst.IdiomsMaxTryCount)
            {
                // 移除活动缓存
                database.KeyDelete(CacheConst.GetIdiomsKey(activityKey));
                return "尝试次数已用完，欢迎下次再来挑战!";
            }
            else
            {
                return $"还剩下{(RuleConst.IdiomsMaxTryCount - tryCount).ToString()} 尝试次数!";
            }

        }

    }
}