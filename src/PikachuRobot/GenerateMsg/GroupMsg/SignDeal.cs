using IServiceSupply;
using Services.PikachuSystem;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 16:52:54
    /// @source : 
    /// @des : 签到
    /// </summary>
    public class SignDeal : IGenerateGroupMsgDeal
    {

        private readonly Random _random = new Random();

        public SignDeal(BillFlowService billFlowService,MemberInfoService memberInfoService)
        {
            BillFlowService = billFlowService;
            MemberInfoService = memberInfoService;
        }

        private BillFlowService BillFlowService { get; }
        private MemberInfoService MemberInfoService { get; }

        public async Task<GroupRes> Run(string msg, string account, string groupNo, Lazy<string> getLoginAccount)
        {
            if ("打卡".Equals(msg) || "签到".Equals(msg) || "冒泡".Equals(msg))
            {

                var now = DateTime.Now;
                var today = new DateTime(now.Year, now.Month, now.Day);
                var tomorrow = today.AddDays(1);

                if (await BillFlowService.GetAll().AnyAsync(u => u.Group == groupNo && u.Account == account
                  && u.BillType == Data.Pikachu.Menu.BillTypes.Sign && u.CreateTime >= today && u.CreateTime < tomorrow))
                {
                    return "已签到";
                }

                var amount = 0;
                var desc = string.Empty;

                var firstDay = new DateTime(now.Year, now.Month, 1);
                var nextMon = firstDay.AddMonths(1);

                var count = await BillFlowService.GetAll().CountAsync(u=>u.Group == groupNo && u.Account == account
                 && u.BillType == Data.Pikachu.Menu.BillTypes.Sign && u.CreateTime >= firstDay && u.CreateTime < nextMon);

                if (await BillFlowService.GetAll().AnyAsync(u => u.Group == groupNo
                   && u.BillType == Data.Pikachu.Menu.BillTypes.Sign && u.CreateTime >= today && u.CreateTime < tomorrow))
                {
                    amount = _random.Next((count + 1) * 10);
                    desc = "普通签到";
                }
                else
                {
                    amount = _random.Next((count + 1) * 50);
                    desc = "首签";
                }

                amount += 5 + now.Month;

                await BillFlowService.AddBillAsync(groupNo, account, amount, amount, Data.Pikachu.Menu.BillTypes.Sign, desc);

                await MemberInfoService.ChangeAmountAsync(groupNo, account, amount);

                return $"签到成功，此次签到共获取{amount}钻石!";

            }

            return null;
        }
    }
}
