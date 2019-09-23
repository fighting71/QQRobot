using Data.Pikachu;
using IServiceSupply;
using Newbe.Mahua.MahuaEvents;
using NLog;
using System.Linq;

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

        private readonly IGroupMsgDeal _groupMsgDeal;
        private PikachuDataContext dbContext;

        public GroupMessageReceivedMahuaEvent(IMahuaApi mahuaApi, IGroupMsgDeal groupMsgDeal, PikachuDataContext dbContext)
        {
            _mahuaApi = mahuaApi;
            _groupMsgDeal = groupMsgDeal;
            this.dbContext = dbContext;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            _logger.Debug($"[receiver][group][msg][{context.FromGroup}]:{context.Message}");

            var loginQq = _mahuaApi.GetLoginQq();
            var list = dbContext.GroupMsgCopys.Where(u => u.FromGroup.Equals(context.FromGroup) && u.Person.Equals(loginQq));

            foreach (var item in list)
            {
                _mahuaApi.SendGroupMessage(item.TargetGroup)
                    .Text(context.Message)
                    .Done();
            }

            if (!dbContext.GroupAuths.Where(u => u.Enable && u.GroupNo.Equals(context.FromGroup)).Any())// 群号尚未授权
            {
                return;
            }

            if (context.Message.Contains($"[@{loginQq}]"))
            {
                context.Message = context.Message.Replace($"[@{loginQq}]", "");

                var res = _groupMsgDeal.Run(context, _mahuaApi);

                if (!string.IsNullOrWhiteSpace(res))
                    _mahuaApi.SendGroupMessage(context.FromGroup).Text(res).Done();

            }
        }
    }
}