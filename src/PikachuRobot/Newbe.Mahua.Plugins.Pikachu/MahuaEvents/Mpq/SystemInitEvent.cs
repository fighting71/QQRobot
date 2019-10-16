using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Newbe.Mahua.NativeApi;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using Newbe.Mahua.Plugins.Pikachu.Domain.Extension.Mpq;
using NLog;
using PikachuRobot.Job.Hangfire;
using PikachuRobot.Job.Hangfire.Job;
using Services.PikachuSystem;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents.Mpq
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/14 10:21:49
    /// @source : 
    /// @des : 
    /// </summary>
    public class SystemInitEvent : ISystemInitEvent
    {
        public SystemInitEvent(IMpqApi mpqApi, IWebHost webHost, IMahuaApi mahuaApi, JobConfigService jobConfigService,
            CustomerJob customerJob)
        {
            this._mpqApi = mpqApi;
            _webHost = webHost;
            _mahuaApi = mahuaApi;
            _jobConfigService = jobConfigService;
            _customerJob = customerJob;
        }

        private static readonly Logger Logger = LogManager.GetLogger(nameof(SystemInitEvent));
        private readonly IMpqApi _mpqApi;
        private readonly IWebHost _webHost;
        private readonly IMahuaApi _mahuaApi;
        private readonly JobConfigService _jobConfigService;
        private readonly CustomerJob _customerJob;

        public async Task Handle()
        {
            Logger.Info("开始初始化mqp插件");

            try
            {
                //var apiGetQQlist = _mpqApi.Api_GetQQlist();

                //Logger.Debug("当前登录的qq列表:" + apiGetQQlist);

                //if (_mpqApi.Api_AddQQ("2758938447", "study2018/", true)) 
                //{ 
                //    Logger.Debug("添加默认账号成功！"); 
                //} 测试失败...

                await _webHost.StartAsync(ConfigConst.HangFireBaseUrl, _mahuaApi.GetSourceContainer());
                Logger.Debug("开启hangfire成功！");

                //await new TestJob().StartAsync();

                //await new AutoCloseGroupActivityJob(null, null).StartAsync();
                //await new AutoOutGroupMsg(null).StartAsync(context.FromGroup, loginQq);

                var list = await _jobConfigService.GetListAsync();

                foreach (var item in list)
                {
                    await _customerJob.StartJob(item);
                }

                Logger.Debug("添加job成功");
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.Info("mqp插件初始化完毕"); // 测试成功
            
        }
    }
}