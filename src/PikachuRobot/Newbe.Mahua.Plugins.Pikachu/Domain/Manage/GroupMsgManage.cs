using IServiceSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.Mahua.MahuaEvents;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 14:44:54
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupMsgManage : BaseList<Func<DelGroupMsgDeal>>, IGroupMsgDeal
    {

        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
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