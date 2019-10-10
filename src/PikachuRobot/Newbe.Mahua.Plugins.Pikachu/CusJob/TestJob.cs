using Autofac;
using Hangfire;
using Newbe.Mahua.Messages;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.CusJob
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/10 14:35:42
    /// @source : 
    /// @des : 
    /// </summary>
    public class TestJob
    {

        private static readonly Logger Logger = LogManager.GetLogger(nameof(TestJob));
        private readonly ILifetimeScope lifetimeScope;

        public TestJob(ILifetimeScope lifetimeScope, IContainerSaver containerSaver, IMahuaApi mahuaApi)
        {
            this.lifetimeScope = lifetimeScope;
        }

        private string GetId(string group)
        {
            return "test.job.group." + group;
        }

        public IMahuaApi MahuaApi { get; }

        public Task Start(string group)
        {
            // 添加定时任务
            RecurringJob.AddOrUpdate(GetId(group), () => SendMessage(group), "*/5 * * * * *");

            return Task.FromResult(0);

        }

        public void SendMessage(string group)
        {
            try
            {
                lifetimeScope.BeginLifetimeScope().Resolve<IGroupMessageFlowFactory>().Begin(group).Text("定时消息！").Done();
                // no register ...
                //MahuaApi.SendGroupMessage(group, "定时消息");
                //MahuaApi.SendGroupMessage(group).Text("定时消息！").Done();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            try
            {
                MahuaApi.SendGroupMessage(group).Text("定时消息！").Done();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

        }

        public Task StopAsnyc(string group)
        {
            // 移除定时任务
            RecurringJob.RemoveIfExists(GetId(group));
            return Task.FromResult(0);
        }

    }
}
