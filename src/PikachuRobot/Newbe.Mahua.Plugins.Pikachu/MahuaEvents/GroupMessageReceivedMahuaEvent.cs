using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Data.Pikachu;
using GenerateMsg.CusConst;
using IServiceSupply;
using Newbe.Mahua.MahuaEvents;
using Newtonsoft.Json;
using NLog;
using StackExchange.Redis;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 发送群消息
    /// </summary>
    public class GroupMessageReceivedMahuaEvent
        : IGroupMessageReceivedMahuaEvent
    {
        private static readonly Logger Logger = LogManager.GetLogger(nameof(GroupMessageReceivedMahuaEvent));

        private readonly IMahuaApi _mahuaApi;
        private readonly IGenerateGroupMsgDeal _generateGroupMsgDeal;

        private readonly PikachuDataContext _dbContext;
        private readonly IDatabase _database;

        public GroupMessageReceivedMahuaEvent(IMahuaApi mahuaApi, IGenerateGroupMsgDeal generateGroupMsgDeal,
            PikachuDataContext dbContext, IDatabase database)
        {
            _mahuaApi = mahuaApi;
            _generateGroupMsgDeal = generateGroupMsgDeal;
            this._dbContext = dbContext;
            this._database = database;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            Logger.Debug($"[receiver][group][msg][{context.FromGroup}]:{context.Message}");

            var loginQq = _mahuaApi.GetLoginQq();

            _ = GroupMsgCopy(context, loginQq);

            _ = ShowCacheMsg(context, loginQq);

            if (!_dbContext.GroupAuths.Any(u => u.Enable && u.GroupNo.Equals(context.FromGroup))) // 群号尚未授权
                return;

            if (context.Message.Contains($"[@{loginQq}]"))
            {
                context.Message = context.Message.Replace($"[@{loginQq}]", "").Trim();

                // mmp 底层做了释放处理 走异步会面临 mahuaApi被释放问题
                // emm... 
                Run(context, loginQq).Wait();
            }
            else
            {
                _mahuaApi.SendGroupMessage(context.FromGroup).Text(context.Message).Done();
            }
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
            var list = await _dbContext.GroupMsgCopys.Where(u =>
                u.FromGroup.Equals(context.FromGroup) && u.Person.Equals(loginQq)).ToListAsync();

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