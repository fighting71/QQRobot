using Data.Pikachu;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
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
    /// @since : 2019/9/25 17:13:56
    /// @source : 
    /// @des : 
    /// </summary>
    public class MemberAmountDeal : IGroupMsgDeal
    {

        private readonly PikachuDataContext pikachuDataContext;

        public MemberAmountDeal(PikachuDataContext pikachuDataContext)
        {
            this.pikachuDataContext = pikachuDataContext;
        }

        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*[查看积分|我的钱包][\s|\n|\r]*$"))
            {
                var memberInfo = pikachuDataContext.MemberInfos.FirstOrDefault(u => u.Group == context.FromGroup && u.Account == context.FromQq);

                if (memberInfo == null)
                {

                    var list = pikachuDataContext.BillFlows.Where(u => u.Group == context.FromGroup && u.Account == context.FromQq);

                    decimal amount = list.Where(u => u.Enable && u.BillType == Data.Pikachu.Menu.BillTypes.Sign).Sum(u => u.ActualAmount)
                        - list.Where(u => u.BillType == Data.Pikachu.Menu.BillTypes.Consume).Sum(u => u.ActualAmount);

                    memberInfo = new Data.Pikachu.Models.MemberInfo()
                    {
                        Account = context.FromQq,
                        Group = context.FromGroup,
                        Amount = amount,
                        CreateTime = DateTime.Now
                    };

                    pikachuDataContext.MemberInfos.Add(memberInfo);

                    pikachuDataContext.SaveChanges();

                }

                return $"当前余额为:{memberInfo.Amount}钻石";

            }

            return string.Empty;
        }
    }
}
