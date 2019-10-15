using System;
using Data.Pikachu;
using Newbe.Mahua.MahuaEvents;
using System.Linq;
using IServiceSupply;
using NLog;
using System.Threading.Tasks;
using Newbe.Mahua.Plugins.Pikachu.Domain.Manage;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {
        private static readonly Logger
            Logger = LogManager.GetLogger(nameof(PrivateMessageFromFriendReceivedMahuaEvent));

        private readonly IMahuaApi _mahuaApi;
        private readonly IGeneratePrivateMsgDeal _generatePrivateMsgDeal;

        private readonly PikachuDataContext dbContext;

        public PrivateMessageFromFriendReceivedMahuaEvent(IMahuaApi mahuaApi,
            PrivateMsgManage generatePrivateMsgDeal, PikachuDataContext dbContext
        )
        {
            _mahuaApi = mahuaApi;
            _generatePrivateMsgDeal = generatePrivateMsgDeal;
            this.dbContext = dbContext;
        }

        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
        {
            if (dbContext.Managers.FirstOrDefault(u => u.Enable && u.Account.Equals(context.FromQq)) == null) // 非管理员
            {
                return;
            }

            _ = Run(context);
        }

        private async Task Run(PrivateMessageFromFriendReceivedContext context)
        {
            context.Message = context.Message.Trim();

            var res = await _generatePrivateMsgDeal
                .Run(context.Message, context.FromQq, (new Lazy<string>((() => _mahuaApi.GetLoginQq()))));

            if (res != null)
            {
                foreach (var item in res.Data)
                {
                    var msg = _mahuaApi.SendPrivateMessage(context.FromQq);
                    if (item.CallTa)
                    {
                        msg.Shake().Done();
                    }

                    msg.Text(item.Msg).Done();
                }
            }
        }
    }
}