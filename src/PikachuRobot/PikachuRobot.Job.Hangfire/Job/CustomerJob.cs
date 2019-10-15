using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu.Menu;
using Data.Pikachu.Models;
using Hangfire;
using NLog;
using Services.PikachuSystem;

namespace PikachuRobot.Job.Hangfire.Job
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/15 16:12:52
    /// @source : 
    /// @des : 
    /// </summary>
    public class CustomerJob
    {
        private readonly JobConfigService _jobConfigService;
        private static readonly Logger Logger = LogManager.GetLogger(nameof(CustomerJob));

        private string GetId(int id)
        {
            return $"customer.confirm.task.{id}";
        }

        public CustomerJob(JobConfigService jobConfigService)
        {
            _jobConfigService = jobConfigService;
        }

        public Task StartJob(JobConfig config)
        {
            RecurringJob.AddOrUpdate(GetId(config.Id), () => DealMsg(config), config.CronExpression);

            return Task.FromResult(0);
        }

        public async Task DealMsg(JobConfig config)
        {
            Logger.Debug($"[自定义任务]开始执行-{config.Id}");

            var dbConfig = await _jobConfigService.GetAsync(config.Id);
            
            if (dbConfig == null || dbConfig.Enable == false) // 任务失效或被删除时 自动停止并移除任务
            {
                Logger.Debug($"[自定义任务]任务已失效-{config.Id}");
                RecurringJob.RemoveIfExists(GetId(config.Id));
                return;
            }

            if (!dbConfig.CronExpression.Equals(config.CronExpression))
            {
                await StartJob(dbConfig);
                Logger.Debug($"[自定义任务]任务配置Cron发生变化,重新执行任务-{config.Id}");
                return;
            }

            config = dbConfig;
            
            var mahuaApi = MahuaApiHelper.CreateApi(config.RobotAccount);

            if (mahuaApi == null)
            {
                Logger.Debug($"[自定义任务]机器人账号尚未登录-{config.Id}");
                RecurringJob.RemoveIfExists(GetId(config.Id));
                return;
            }

            if (config.ConfirmType == ConfirmTypes.FriendConfirm)
            {
                mahuaApi.SendPrivateMessage(config.Target, config.Template);
            }
            else if (config.ConfirmType == ConfirmTypes.GroupConfirm)
            {
                string message = (string.IsNullOrWhiteSpace(config.Target) ? "" : $"[@{config.Target}]") +
                                 config.Template;
                mahuaApi.SendGroupMessage(config.GroupNo, message);
            }
            else if (config.ConfirmType == ConfirmTypes.NoticeConfirm)
            {
                mahuaApi.SetNotice(config.GroupNo, config.Title, config.Template);
            }

            Logger.Debug($"[自定义任务]任务执行完毕-{config.Id}");
        }
    }
}