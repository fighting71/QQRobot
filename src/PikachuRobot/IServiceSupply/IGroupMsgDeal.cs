using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;

namespace IServiceSupply
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 14:54:47
    /// @source : 
    /// @des : 
    /// </summary>
    public interface IGroupMsgDeal
    {
        GroupRes Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi);
    }
    
    public delegate GroupRes DelGroupMsgDeal(GroupMessageReceivedContext context, IMahuaApi mahuaApi);
    
}