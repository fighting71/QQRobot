using Newbe.Mahua.MahuaEvents;
using NLog;
using System;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 菜单点击事件
    /// </summary>
    public class MahuaMenuClickedMahuaEvent
        : IMahuaMenuClickedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        private static Logger _logger = LogManager.GetLogger(nameof(MahuaMenuClickedMahuaEvent));

        public MahuaMenuClickedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessManhuaMenuClicked(MahuaMenuClickedContext context)
        {

            _logger.Info($"菜单-{context.Menu}被点击");

            // todo 填充处理逻辑
            //throw new NotImplementedException();

            // 不要忘记在MahuaModule中注册
        }
    }
}
