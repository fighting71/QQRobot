using Newbe.Mahua.Apis;
using NLog;

namespace Newbe.Mahua.Samples.ApiExtensions.MahuaApis
{
    /// <summary>
    /// 发送名片赞
    /// </summary>
    public class SendLikeApiMahuaCommandHandler1
        : IApiCommandHandler<SendLikeApiMahuaCommand>
    {

        private static readonly Logger _logger = LogManager.GetLogger(nameof(SendLikeApiMahuaCommandHandler1));

        private readonly IMahuaApi _mahuaApi;

        public SendLikeApiMahuaCommandHandler1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void Handle(SendLikeApiMahuaCommand message)
        {

            _logger.Debug($"接收到名片赞消息:{message.ToQq}");

            // todo 填充处理逻辑
            //throw new NotImplementedException();

            // 不要忘记在MahuaModule中注册
        }
    }
}

