using Newbe.Mahua.MahuaEvents;
using NLog;
using PikachuRobot.Job.Hangfire;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 插件初始化事件
    /// mypcqq free 无效... 
    /// 查找源码 此事件跟随着热更新触发。。 即 hash.txt 文件变更，启动热更新。
    /// 跟系统初始化没太多关系 [mpq是这样的]
    /// </summary>
    public class InitializationMahuaEvent
        : IInitializationMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly IWebHost _webHost;
        private static readonly Logger _logger = LogManager.GetLogger(nameof(InitializationMahuaEvent));

        public InitializationMahuaEvent(
            IMahuaApi mahuaApi, IWebHost webHost)
        {
            _mahuaApi = mahuaApi;
            this._webHost = webHost;
        }

        public void Initialized(InitializedContext context)
        {

            _logger.Info("插件初始化完成！");

            _webHost.StartAsync("http://localhost:65271", _mahuaApi.GetSourceContainer());

            // todo 填充处理逻辑
            //throw new NotImplementedException();

            // 不要忘记在MahuaModule中注册
        }
    }
}
