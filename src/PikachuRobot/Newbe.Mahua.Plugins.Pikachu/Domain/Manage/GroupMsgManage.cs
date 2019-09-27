using IServiceSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.Mahua.MahuaEvents;
using NLog;

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

        private static Logger _logger = LogManager.GetLogger(nameof(GroupMsgManage));

        public GroupRes Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {
            GroupRes res = null;
            for (int i = 0; i < list.Count && res == null; i++)
            {
                try
                {
                    res = list[i]()(context, mahuaApi);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                }
            }

            return res;
        }
    }
}