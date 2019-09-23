using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceSupply
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 10:33:39
    /// @source : 
    /// @des : 
    /// </summary>
    public interface IPrivateMsgDeal
    {

        string Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi);

    }
    
    public delegate string DelPrivateMsgDeal(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi);
}
