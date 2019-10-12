using System;
using System.Threading.Tasks;
using Hangfire;
using Newbe.Mahua;
using NLog;

namespace PikachuRobot.Job.Hangfire.Job
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/10 14:35:42
    /// @source : 
    /// @des : 测试job
    /// </summary>
    public class TestJob
    {
        private static readonly Logger Logger = LogManager.GetLogger(nameof(TestJob));

        private string GetId(string group, string account)
        {
            return "test.job.group." + group;
        }

        public Task StartAsync(string group, string account)
        {
            // 添加定时任务 15s/次
            RecurringJob.AddOrUpdate(GetId(group, account), () => SendMessage(group, account), "* */1 * * * *");

            return Task.FromResult(0);
        }

        // only public method can be able to call
        public void SendMessage(string group, string account)
        {
            // 由于注入的scope 不同 无法获取到完整信息(生命周期不一致...) 所以mahuaapi需要重新创建.
            var session = MahuaRobotManager.Instance.CreateSession(account);
            try
            {
                // 添加定时任务 15s/次
                session.MahuaApi.SendGroupMessage(group).Text($"当前时间:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}")
                    .Done();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public Task StopAsync(string group, string account)
        {
            // 移除定时任务
            RecurringJob.RemoveIfExists(GetId(group, account));
            return Task.FromResult(0);
        }
    }
}