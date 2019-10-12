using System;
using System.Threading.Tasks;
using Domain.Command.CusConst;
using Hangfire;
using IServiceSupply;
using Newbe.Mahua;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace PikachuRobot.Job.Hangfire.Job
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/12 16:24:23
    /// @source : 
    /// @des : 自动关闭活动任务
    /// </summary>
    public class AutoOutGroupMsg
    {
        private readonly IDatabase _database;

        public AutoOutGroupMsg(IDatabase database)
        {
            _database = database;
        }

        private static string GetJobId(string groupNo)
        {
            return $"group.out.msg.{groupNo}";
        }

        public Task StartAsync(string groupNo, string account)
        {
            RecurringJob.AddOrUpdate(GetJobId(groupNo), () => DealMessage(groupNo, account), "* */1 * * * *");

            return Task.FromResult(0);
        }

        public static Task StopAsync(string groupNo)
        {
            // 移除任务
            RecurringJob.RemoveIfExists(GetJobId(groupNo));
            return Task.FromResult(0);
        }

        public async Task DealMessage(string groupNo, string account)
        {
            try
            {

                var key = CacheConst.GetGroupMsgListKey(groupNo);

                string cacheMsgInfo = await _database.ListLeftPopAsync(key);

                IMahuaApi mahuaApi = null;

                while (!string.IsNullOrWhiteSpace(cacheMsgInfo))
                {
                    mahuaApi = mahuaApi ?? MahuaRobotManager.Instance.CreateSession(account).MahuaApi;

                    GroupItemRes msg = JsonConvert.DeserializeObject<GroupItemRes>(cacheMsgInfo);

                    MahuaApiHelper.SendGroupMsg(mahuaApi, msg, groupNo, account);

                    cacheMsgInfo = await _database.ListLeftPopAsync(key);
                }
            }
            catch (Exception e)
            {
                var str = e.Message;
            }
        }

    }
}