using System.Threading.Tasks;
using Domain.Command.CusConst;
using Hangfire;
using IServiceSupply;
using Newtonsoft.Json;
using Services.PikachuSystem;
using StackExchange.Redis;

namespace PikachuRobot.Job.Hangfire.Job
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/12 16:24:23
    /// @source : 
    /// @des : 自动关闭活动任务
    /// </summary>
    public class AutoCloseGroupActivityJob
    {
        private readonly GroupActivityService _groupActivityService;
        private readonly IDatabase _database;

        public AutoCloseGroupActivityJob(GroupActivityService groupActivityService, IDatabase database)
        {
            _groupActivityService = groupActivityService;
            _database = database;
        }

        private static string GetJobId()
        {
            return $"group.activity.autoClose";
        }

        public Task StartAsync()
        {
            // 添加定时任务 分钟/次
            RecurringJob.AddOrUpdate(GetJobId(), () => DealMessage(), "*/1 * * * *");

            return Task.FromResult(0);
        }

        public static Task StopAsync()
        {
            // 移除任务
            RecurringJob.RemoveIfExists(GetJobId());
            return Task.FromResult(0);
        }

        public async Task DealMessage()
        {
            var list = await _groupActivityService.AutoCloseAsync();

            foreach (var item in list)
            {
                var key = CacheConst.GetGroupMsgListKey(item.Group);

                _database.ListLeftPush(key,
                    JsonConvert.SerializeObject(new GroupItemRes()
                    {
                        Msg = $@"
>>>>>>>>>成语接龙已结束<<<<<<<<<<<<
    成功次数:{item.SuccessCount.ToString()}
    失败次数:{item.FailureCount.ToString()}
希望大家再接再厉！
"
                    }));
            }
        }
    }
}