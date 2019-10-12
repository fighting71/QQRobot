using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServiceSupply;
using Newbe.Mahua;

namespace PikachuRobot.Job.Hangfire
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/12 17:40:12
    /// @source : 
    /// @des : 
    /// </summary>
    public class MahuaApiHelper
    {
        public static void SendGroupMsg(IMahuaApi mahuaApi, GroupItemRes item, string group, string account)
        {
            var msg = mahuaApi.SendGroupMessage(group);
            if (item.AtAll)
            {
                msg.AtlAll().Text(item.Msg).Done();
            }
            else if (item.AtTa)
            {
                msg.At(account).Text(item.Msg).Done();
            }
            else
            {
                msg.Text(item.Msg).Done();
            }
        }
    }
}