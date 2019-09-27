using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServiceSupply;
using Newbe.Mahua.MahuaEvents;
using NLog;

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

        private static Logger _logger = LogManager.GetLogger(nameof(PrivateMsgManage));

        public PrivateRes Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            PrivateRes res = null;
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