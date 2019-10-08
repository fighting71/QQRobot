using GenerateMsg.CusConst;
using IServiceSupply;
using Services.PikachuSystem;
using Services.Utils;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 16:59:15
    /// @source : 
    /// @des : 成语接龙处理[考虑缓存]
    /// </summary>
    public class IdiomsSolitaireCacheDeal : IGenerateGroupMsgDeal
    {
        private readonly IDatabase _database;
        private readonly Random _random = new Random();

        public IdiomsSolitaireCacheDeal(IdiomsService idiomsService, IDatabase database, BillFlowService billFlowService
            , MemberInfoService memberInfoService, ActivityLogService activityLogService, ManageService manageService)
        {
            IdiomsService = idiomsService;
            this._database = database;
            BillFlowService = billFlowService;
            MemberInfoService = memberInfoService;
            ActivityLogService = activityLogService;
            ManageService = manageService;
        }

        private IdiomsService IdiomsService { get; }
        private BillFlowService BillFlowService { get; }
        private MemberInfoService MemberInfoService { get; }
        private ActivityLogService ActivityLogService { get; }
        public ManageService ManageService { get; }

        public async Task<GroupRes> Run(string msg, string account, string groupNo, Lazy<string> getLoginAccount)
        {
            var activityKey = CacheConst.GetGroupActivityKey(groupNo);

            var groupActivity = _database.StringGet(activityKey);

            if (CacheConst.IdiomsSolitaire.Equals(groupActivity))
            {

                var idiomsKey = CacheConst.GetIdiomsKey(groupNo);
                var idiomsTryCountKey = CacheConst.GetIdiomsTryCountKey(groupNo);

                var logId = int.Parse(await _database.StringGetAsync(CacheConst.GetActivityLogKey(groupNo)));

                var activityLog = ActivityLogService.GetActivity(logId);

                if(activityLog.ActivityStateType == Data.Pikachu.Menu.ActivityStateTypes.Close)
                {
                    // 移除活动缓存
                    await _database.KeyDeleteAsync(activityKey);

                    return "成语接龙活动已关闭!";
                }


                if ("关闭活动".Equals(msg) && await ManageService.IsManageAsync(account))// 管理员主动结束活动.
                {
                    // 移除活动缓存
                    await _database.KeyDeleteAsync(activityKey);

                    var log = await ActivityLogService.CloseActivityAsync(logId, "活动结束，管理员主动结束活动!");

                    return $@"
>>>>>>>>>本次活动已结束，欢迎下次再来挑战!<<<<<<<<<<<<
本次挑战成果:
    成功次数:{log.SuccessCount.ToString()}
    失败次数:{log.FailureCount.ToString()}
希望大家再接再厉！
";
                }

                // 增加尝试次数
                var tryCount = await _database.StringIncrementAsync(idiomsTryCountKey);

                var idiomId = await _database.StringGetAsync(idiomsKey);

                var spell = await IdiomsService.GetInfoAsync(int.Parse(idiomId));

                var confrimStr = $@"
>>>>>>>>>成语接龙火热进行中<<<<<<<<<<<<
    当前成语:{spell.Word}
    尾拼:{spell.LastSpell}
    成语解析:{spell.Explanation}
";

                if (msg.Length != 4)
                {
                    return GroupRes.GetSuccess(new GroupItemRes() {AtTa = true, Msg = "输入格式有误！"},
                        await GetTryCountRes(tryCount, activityKey, logId), confrimStr);
                }

                var info = await IdiomsService.GetByWordAsync(msg);

                if (info == null)
                {
                    return GroupRes.GetSuccess(new GroupItemRes() {AtTa = true, Msg = "词语输入有误！"},
                        await GetTryCountRes(tryCount, activityKey, logId), confrimStr);
                }

                if (!spell.LastSpell.Equals(info.FirstSpell))
                {
                    return GroupRes.GetSuccess(new GroupItemRes() {AtTa = true, Msg = "你输入的词语并不能接上呢！"},
                        await GetTryCountRes(tryCount, activityKey, logId), confrimStr);
                }

                // 积分奖励
                var amount = _random.Next(100) + 5;

                await ActivityLogService.AddSuccessCountAsync(logId);

                await BillFlowService.AddBillAsync(groupNo, account, amount, amount, Data.Pikachu.Menu.BillTypes.Reward,
                    "成语接龙奖励");

                await MemberInfoService.ChangeAmountAsync(groupNo, account, amount);

                // 重新缓存
                await _database.StringSetAsync(idiomsKey, info.Id, RuleConst.GroupActivityExpiry);

                // 重新缓存
                await _database.StringSetAsync(idiomsTryCountKey, 0,
                    RuleConst.GroupActivityExpiry);


                return GroupRes.GetSuccess(new GroupItemRes() {AtTa = true, Msg = $"   恭喜你获得{amount.ToString()}钻石奖励！"},
                    $@"
>>>>>>>>>【成语接龙】下一回合<<<<<<<<<<<<
    当前成语:{info.Word}
    尾拼:{info.LastSpell}
    成语解析:{info.Explanation}
"
                );
            }

            return null;
        }

        private async Task<string> GetTryCountRes(long tryCount, string activityKey, int logId)
        {
            await ActivityLogService.AddFailureCountAsync(logId);

            if (tryCount == RuleConst.IdiomsMaxTryCount)
            {
                // 移除活动缓存
                await _database.KeyDeleteAsync(activityKey);

                var log = await ActivityLogService.CloseActivityAsync(logId, "活动结束，挑战次数使用完毕!");

                return $@"
>>>>>>>>>尝试次数已用完，欢迎下次再来挑战!<<<<<<<<<<<<
本次挑战成果:
    成功次数:{log.SuccessCount.ToString()}
    失败次数:{log.FailureCount.ToString()}
希望大家再接再厉！
";
            }

            return $"还剩下{(RuleConst.IdiomsMaxTryCount - tryCount).ToString()} 尝试次数!";
        }
    }
}