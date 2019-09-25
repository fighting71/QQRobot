using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServiceSupply;
using Newbe.Mahua.MahuaEvents;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 13:46:23
    /// @source : 
    /// @des : 
    /// </summary>
    public class PrivateMsgManage : BaseList<Func<DelPrivateMsgDeal>>, IPrivateMsgDeal
    {
        public string Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            string res = string.Empty;
            for (int i = 0; i < list.Count && res == string.Empty; i++)
            {
                res = list[i]()(context, mahuaApi);
            }

            return res;
        }
    }
}