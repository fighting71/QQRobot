using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Services.PikachuSystem;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 16:52:54
    /// @source : 
    /// @des : 
    /// </summary>
    public class SignDeal : IGroupMsgDeal
    {

        private Random random = new Random();

        public SignDeal(BillFlowService billFlowService,MemberInfoService memberInfoService)
        {
            BillFlowService = billFlowService;
            MemberInfoService = memberInfoService;
        }

        public BillFlowService BillFlowService { get; }
        public MemberInfoService MemberInfoService { get; }

        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            var math = Regex.Match(context.Message, @"^[\s|\n|\r]*(.{2})[\s|\n|\r]*$");

            if (!math.Success) return string.Empty;

            var info = math.Groups[1].Value;

            if ("打卡".Equals(info) || "签到".Equals(info) || "冒泡".Equals(info))
            {

                var now = DateTime.Now;
                var today = new DateTime(now.Year, now.Month, now.Day);
                var tomorrow = today.AddDays(1);

                if (BillFlowService.GetAll().Any(u => u.Group == context.FromGroup && u.Account == context.FromQq
                  && u.BillType == Data.Pikachu.Menu.BillTypes.Sign && u.CreateTime >= today && u.CreateTime < tomorrow))
                {
                    return "已签到";
                }

                var amount = 0;
                var desc = string.Empty;

                var firstDay = new DateTime(now.Year, now.Month, 1);
                var nextMon = firstDay.AddMonths(1);

                var count = BillFlowService.GetAll().Count(u=>u.Group == context.FromGroup && u.Account == context.FromQq
                 && u.BillType == Data.Pikachu.Menu.BillTypes.Sign && u.CreateTime >= firstDay && u.CreateTime < nextMon);

                if (BillFlowService.GetAll().Any(u => u.Group == context.FromGroup
                   && u.BillType == Data.Pikachu.Menu.BillTypes.Sign && u.CreateTime >= today && u.CreateTime < tomorrow))
                {
                    amount = random.Next((count + 1) * 10);
                    desc = "普通签到";
                }
                else
                {
                    amount = random.Next((count + 1) * 50);
                    desc = "首签";
                }

                amount += 5 + now.Month;

                BillFlowService.AddBill(context.FromGroup, context.FromQq, amount, amount, Data.Pikachu.Menu.BillTypes.Sign, desc);

                MemberInfoService.ChangeAmount(context.FromGroup, context.FromQq, amount);

                return $"签到成功，此次签到共获取{amount}钻石!";

            }

            return string.Empty;
        }
    }
}
