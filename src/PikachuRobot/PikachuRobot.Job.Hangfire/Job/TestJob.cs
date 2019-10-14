using System;
using System.Threading.Tasks;
using Autofac;
using Hangfire;
using Newbe.Mahua;
using Newbe.Mahua.NativeApi;
using NLog;

namespace Newbe.Mahua
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

        public TestJob()
        {
        }

        private string GetId()
        {
            return "empty.test.job";
        }

        public Task StartAsync()
        {
            // 添加定时任务 15s/次
            RecurringJob.AddOrUpdate(GetId(), () => SendMessage(), "* */1 * * * *");

            return Task.FromResult(0);
        }

        // only public method can be able to call
        public void SendMessage()
        {
            try
            {
                TestFun();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// 测试
        /// </summary>
        private void TestFun()
        {
            // 此处可直接通过mahua api获取
            //var apiGetQQlist = _mpqApi.Api_GetQQlist();

            // 由于注入的scope 不同 无法获取到完整信息(生命周期不一致...) 所以mahuaapi需要重新创建.
            // ... lj api 
            /*
             *
             * 查看源码
             *
             * /// <summary>创建会话</summary>
               * /// <returns></returns>
               * /// <remarks>多QQ机器人平台则返回第一个QQ</remarks>
               * IRobotSession CreateSession();
            
               * /// <summary>创建会话</summary>
               * /// <param name="qq">指定的QQ机器人</param>
               * /// <returns></returns>
               * /// <exception cref="T:Newbe.Mahua.QqNotFoundException">指定的QQ不存在</exception>
               * IRobotSession CreateSession(string qq);
             * 
             * 其中第二个使用了if (!this._qqProvider.CheckQq(qq)) throw new QqNotFoundException(qq); 检测
             * deep:   return this.DefaultQqProvider() == qq;
             * 即只能用默认qq创建
             * 所以CreateSession(string qq) 除了看看是不是默认请求创建Session外 没有任何意义....
             * 而当账号列表存在多个账号时，默认qq会是[qq1/n/rqq2] 这样的多账号拼接 Session也无法使用
             *
             * 以上仅针对mpq 不过api确实有点问题。
             * 注:实现查看 Newbe.Mahua.MahuaRobotManagerImpl
             * 
             */
            var session = MahuaRobotManager.Instance.CreateSession();

            var apiQqList = session.MahuaApi.GetLoginQq();

            if (string.IsNullOrWhiteSpace(apiQqList)) return;

            var qqArr = apiQqList.Split('\n');

            foreach (var qq in qqArr)
            {
                session.LifetimeScope.Resolve<IRobotSessionContext>().CurrentQqProvider =
                    () => qq.Replace("\r", ""); // 为了避免多账号问题 重新指向qq 否则发送消息无效...

                session.MahuaApi.SendGroupMessage("914210975")
                    .Text($"当前时间:{DateTime.Now:yyyy-MM-dd hh:mm:ss} {apiQqList}")
                    .Done();
            }
        }

        public Task StopAsync()
        {
            // 移除定时任务
            RecurringJob.RemoveIfExists(GetId());
            return Task.FromResult(0);
        }
        
    }
    
}