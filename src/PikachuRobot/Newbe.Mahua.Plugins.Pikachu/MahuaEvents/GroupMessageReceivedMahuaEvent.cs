using System;
using System.Threading.Tasks;
using IServiceSupply;
using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using NLog;
using PikachuRobot.Job.Hangfire;
using PikachuRobot.Job.Hangfire.Job;
using Services.PikachuSystem;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 发送群消息
    /// </summary>
    public class GroupMessageReceivedMahuaEvent
        : IGroupMessageReceivedMahuaEvent
    {
        private GroupAuthService GroupAuthService { get; }
        private GroupMsgCopyService GroupMsgCopyService { get; }

        private static readonly Logger Logger = LogManager.GetLogger(nameof(GroupMessageReceivedMahuaEvent));

        private readonly IMahuaApi _mahuaApi;
        private readonly IGenerateGroupMsgDeal _generateGroupMsgDeal;
        private readonly IWebHost _webHost;


        public GroupMessageReceivedMahuaEvent(IMahuaApi mahuaApi, IGenerateGroupMsgDeal generateGroupMsgDeal,
            GroupAuthService groupAuthService, GroupMsgCopyService groupMsgCopyService, IWebHost webHost)
        {
            _mahuaApi = mahuaApi;
            _generateGroupMsgDeal = generateGroupMsgDeal;
            _webHost = webHost;
            GroupAuthService = groupAuthService;
            GroupMsgCopyService = groupMsgCopyService;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            Logger.Debug($"[receiver][group][msg][{context.FromGroup}]:{context.Message}");

            var loginQq = _mahuaApi.GetLoginQq();

            _ = GroupMsgCopy(context, loginQq);

            InitHangFire();

            if (!GroupAuthService.Exists(context.FromGroup)) // 群号尚未授权
                return;

            //if (context.Message.Contains($"[@{loginQq}]"))
            //{
            //    context.Message = context.Message.Replace($"[@{loginQq}]", "").Trim();

            Run(context, loginQq).Wait();
            //}
            //else
            //{
            //    _mahuaApi.SendGroupMessage(context.FromGroup).Text(context.Message).Done();
            //}
        }

        private void InitHangFire()
        {
            if (_webHost.IsOpen()) return;

            _webHost.StartAsync(ConfigConst.HangFireBaseUrl, _mahuaApi.GetSourceContainer());
        }

        /// <summary>
        /// 群消息转载
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loginQq"></param>
        /// <returns></returns>
        private async Task GroupMsgCopy(GroupMessageReceivedContext context, string loginQq)
        {
            // 存在群消息转载
            var list = await GroupMsgCopyService.GetList(loginQq, context.FromGroup);

            foreach (var item in list)
            {
                _mahuaApi.SendGroupMessage(item.TargetGroup)
                    .Text($"[来自{item.FromGroup}的群消息]:{context.Message}")
                    .Done();
            }
        }

        private async Task Run(GroupMessageReceivedContext context, string loginQq)
        {
            context.Message = context.Message.Trim();

            if ("test job".Equals(context.Message))
            {
                await new TestJob().StartAsync(context.FromGroup, loginQq);
                await new AutoCloseGroupActivityJob(null, null).StartAsync(context.FromGroup);
                await new AutoOutGroupMsg(null).StartAsync(context.FromGroup, loginQq);
                return;
            }

            if ("close test job".Equals(context.Message))
            {
                await new TestJob().StopAsync(context.FromGroup, loginQq);
                await AutoCloseGroupActivityJob.StopAsync(context.FromGroup);
                await AutoCloseGroupActivityJob.StopAsync(context.FromGroup);
                return;
            }

            var res = await _generateGroupMsgDeal
                .Run(context.Message, context.FromQq, context.FromGroup,
                    (new Lazy<string>((() => loginQq))));

            if (res != null)
            {
                foreach (var item in res.Data)
                {
                    SendMsg(_mahuaApi, item, context.FromGroup, context.FromQq);
                }
            }
        }

        private void SendMsg(IMahuaApi mahuaApi, GroupItemRes item, string group, string account)
        {
            var msg = mahuaApi.SendGroupMessage(group);
            if (item.AtAll)
            {
                msg.AtlAll().Text(item.Msg).Done();
            }
            else if (item.AtTa)
            {
                msg.At(account).Text(item.Msg).Done();
            }
            else
            {
                msg.Text(item.Msg).Done();
            }
        }
    }
}