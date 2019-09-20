using Newbe.Mahua.Messages;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.ApiExtension
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 11:12:23
    /// @source : 
    /// @des : 
    /// </summary>
    public static class MahuaApiExt
    {

        public static IGroupMessageStep2 SendDefaultGroupMsg(this IGroupMessageStep step,string ta = null)
        {
            return step.At(ta).Text(ConfigConst.DefaultGroupMsg);
        }

        public static IPrivateMessageStep2 SendDefaultPrivateMsg(this IPrivateMessageStep step)
        {
            return step.Text(ConfigConst.DefaultPrivateMsg);
        }

    }
}
