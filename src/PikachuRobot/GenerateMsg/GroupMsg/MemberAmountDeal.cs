using Data.Pikachu;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Services.PikachuSystem;
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


        public MemberAmountDeal(MemberInfoService memberInfoService)
        {
            MemberInfoService = memberInfoService;
        }

        public MemberInfoService MemberInfoService { get; }

        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*[查看积分|我的钱包][\s|\n|\r]*$"))
            {
                var memberInfo = MemberInfoService.GetInfo(context.FromGroup, context.FromQq);

                return $"当前余额为:{memberInfo.Amount}钻石";

            }

            return string.Empty;
        }
    }
}
