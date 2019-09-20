using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua.Plugins.Pikachu.ApiExtension;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using System;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public PrivateMessageFromFriendReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
        {
            // todo 填充处理逻辑
            //throw new NotImplementedException();

            // 不要忘记在MahuaModule中注册

            if (string.IsNullOrWhiteSpace(context.Message))
            {
                _mahuaApi
                    .SendPrivateMessage(context.FromQq)
                    .Text(ConfigConst.DefaultPrivateMsg)
                    .Done();
                //_mahuaApi.SendPrivateMessage(context.FromQq).SendDefaultPrivateMsg().Done();
                return;
            }

            // 戳一戳
            _mahuaApi.SendPrivateMessage(context.FromQq)
                .Shake()
                .Done();

            _mahuaApi.SendPrivateMessage(context.FromQq)
            .Text(context.Message)
            .Done();

        }
    }
}
