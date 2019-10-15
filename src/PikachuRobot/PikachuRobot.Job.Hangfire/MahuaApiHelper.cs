using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
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
        public static IMahuaApi CreateApi(string account)
        {
            var session = MahuaRobotManager.Instance.CreateSession();

            var loginQq = session.MahuaApi.GetLoginQq();

            // 单q 匹配直接返回
            if (loginQq.Equals(account)) return session.MahuaApi;

            // 无匹配账号直接返回null
            if (!loginQq.Contains(account)) return null;

            session.LifetimeScope.Resolve<IRobotSessionContext>().CurrentQqProvider =
                () => account;

            return session.MahuaApi;
        }

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