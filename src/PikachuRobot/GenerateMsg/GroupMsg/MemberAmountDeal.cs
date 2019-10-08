using IServiceSupply;
using Services.PikachuSystem;
using System;
using System.Threading.Tasks;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 17:13:56
    /// @source : 
    /// @des : 成员账户
    /// </summary>
    public class MemberAmountDeal : IGenerateGroupMsgDeal
    {
        public MemberAmountDeal(MemberInfoService memberInfoService)
        {
            MemberInfoService = memberInfoService;
        }

        private MemberInfoService MemberInfoService { get; }

        public async Task<GroupRes> Run(string msg, string account, string groupNo, Lazy<string> getLoginAccount)
        {
            if ("查看积分".Equals(msg) || "我的钱包".Equals(msg))
            {
                var memberInfo = await MemberInfoService.GetInfoAsync(groupNo, account);

                return $"当前余额为:{memberInfo.Amount}钻石";
            }

            return null;
        }
    }
}