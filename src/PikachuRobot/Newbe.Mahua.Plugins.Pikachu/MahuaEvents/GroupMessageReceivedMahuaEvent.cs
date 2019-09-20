using Autofac;
using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua.Messages;
using Newbe.Mahua.Plugins.Pikachu.ApiExtension;
using Newbe.Mahua.Plugins.Pikachu.CusTools;
using Newbe.Mahua.Plugins.Pikachu.Domain.Manage;
using Newtonsoft.Json;
using NLog;

namespace Newbe.Mahua.Samples.ApiExtensions.MahuaApis
{
    /// <summary>
    /// 发送群消息
    /// </summary>
    public class GroupMessageReceivedMahuaEvent
        : IGroupMessageReceivedMahuaEvent
    {
        private static Logger _logger = LogManager.GetLogger(nameof(GroupMessageReceivedMahuaEvent));

        private readonly IMahuaApi _mahuaApi;

        public GroupMessageReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            var msg = context.Message;

            if (string.IsNullOrWhiteSpace(msg))
            {
                return;
            }

            _logger.Debug($"received msg : {msg}");
            var loginQq = _mahuaApi.GetLoginQq();

            if (msg.Contains($"[@{loginQq}]"))
            {
                context.Message = context.Message.Replace($"[@{loginQq}]", "");

                var reply = GroupMsgGroup.GetMsg(_mahuaApi, context);
                if (reply != string.Empty)
                {
                    _mahuaApi.SendGroupMessage(context.FromGroup).Text(reply).Done();
                }
                else
                    _mahuaApi.SendGroupMessage(context.FromGroup).SendDefaultGroupMsg(context.FromQq).Done();
            }
        }
    }
}