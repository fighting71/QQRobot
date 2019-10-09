using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Data.Pikachu;
using GenerateMsg.CusConst;
using IServiceSupply;
using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua.Plugins.Pikachu.Domain.Manage;
using Newtonsoft.Json;
using NLog;
using Services.PikachuSystem;
using StackExchange.Redis;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 发送群消息
    /// </summary>
    public class GroupMessageReceivedMahuaEvent
        : IGroupMessageReceivedMahuaEvent
    {
        public IContainer Container { get; }
        public GroupAuthService GroupAuthService { get; }
        public GroupMsgCopyService GroupMsgCopyService { get; }

        private static readonly Logger Logger = LogManager.GetLogger(nameof(GroupMessageReceivedMahuaEvent));

        private readonly IMahuaApi _mahuaApi;
        private readonly IGenerateGroupMsgDeal _generateGroupMsgDeal;

        private readonly IDatabase _database;
        private readonly IMahuaRobotManager _robotManager;

        public GroupMessageReceivedMahuaEvent(IMahuaApi mahuaApi, IGenerateGroupMsgDeal generateGroupMsgDeal,
            GroupAuthService groupAuthService,GroupMsgCopyService groupMsgCopyService, IDatabase database, IMahuaRobotManager robotManager)
        {
            _mahuaApi = mahuaApi;
            //_mahuaApi = robotManager.CreateSession(mahuaApi.GetLoginQq()).MahuaApi;
            _generateGroupMsgDeal = generateGroupMsgDeal;
            GroupAuthService = groupAuthService;
            GroupMsgCopyService = groupMsgCopyService;
            this._database = database;
            _robotManager = robotManager;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            
            Logger.Debug($"[receiver][group][msg][{context.FromGroup}]:{context.Message}");

            var loginQq = _mahuaApi.GetLoginQq();

            _ = GroupMsgCopy(context, loginQq);

            _ = ShowCacheMsg(context, loginQq);

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

        /// <summary>
        /// 输出缓存消息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ShowCacheMsg(GroupMessageReceivedContext context, string loginQq)
        {
            var key = CacheConst.GetGroupListKey(context.FromGroup, loginQq);

            string cacheMsgInfo = await _database.ListLeftPopAsync(key);

            while (!string.IsNullOrWhiteSpace(cacheMsgInfo))
            {
                GroupItemRes msg = JsonConvert.DeserializeObject<GroupItemRes>(cacheMsgInfo);

                SendMsg(msg, context.FromGroup, context.FromQq);

                cacheMsgInfo = await _database.ListLeftPopAsync(key);
            }
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
            var list = await GroupMsgCopyService.GetList(loginQq,context.FromGroup);

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

            if ("定时消息".Equals(context.Message))
            {
                ThreadPool.QueueUserWorkItem((state =>
                {
                    
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    
                    _robotManager.CreateSession(loginQq).MahuaApi.SendGroupMessage(context.FromGroup)
                        .Text("发送定时消息!").Done();
                }));
                
                return;
            }

            var res = await _generateGroupMsgDeal
                .Run(context.Message, context.FromQq, context.FromGroup,
                    (new Lazy<string>((() => loginQq))));

            if (res != null)
            {
                foreach (var item in res.Data)
                {
                    SendMsg(item, context.FromGroup, context.FromQq);
                }
            }
        }

        private void SendMsg(GroupItemRes item, string group, string account)
        {
            var msg = _mahuaApi.SendGroupMessage(group);
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