using Data.Pikachu;
using Newbe.Mahua.MahuaEvents;
using System.Linq;
using IServiceSupply;
using NLog;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {
        private static Logger _logger = LogManager.GetLogger(nameof(PrivateMessageFromFriendReceivedMahuaEvent));

        private readonly IMahuaApi _mahuaApi;

        private IPrivateMsgDeal _privateMsgDeal;

        private PikachuDataContext dbContext;

        public PrivateMessageFromFriendReceivedMahuaEvent(IMahuaApi mahuaApi, IPrivateMsgDeal privateMsgDeal, PikachuDataContext dbContext)
        {
            _mahuaApi = mahuaApi;
            _privateMsgDeal = privateMsgDeal;
            this.dbContext = dbContext;
        }

        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
        {

            _logger.Debug($"[receiver][private][msg][{context.FromQq}]:{context.Message}");

            if (dbContext.Managers.FirstOrDefault(u => u.Enable && u.Account.Equals(context.FromQq)) == null) // 非管理员
            {
                return;
            }

            var res = _privateMsgDeal.Run(context, _mahuaApi);

            if (!string.IsNullOrWhiteSpace(res))
                _mahuaApi.SendPrivateMessage(context.FromQq)
                    .Text(res)
                    .Done();
        }
    }
}