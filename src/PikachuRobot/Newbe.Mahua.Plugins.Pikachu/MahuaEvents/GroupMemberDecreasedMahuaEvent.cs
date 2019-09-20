using Newbe.Mahua.MahuaEvents;
using System;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 群成员减少事件
    /// </summary>
    public class GroupMemberDecreasedMahuaEvent
        : IGroupMemberDecreasedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GroupMemberDecreasedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMemberDecreased(GroupMemberDecreasedContext context)
        {

            var isAdminOpt = string.IsNullOrWhiteSpace(context.FromQq)?string.Empty: "[管理员操作]";

            _mahuaApi.SendGroupMessage(context.FromGroup)
               .Text($"一位小伙伴悄然离去~{context.ToQq} {isAdminOpt}")
               .Done();

            // todo 填充处理逻辑
            //throw new NotImplementedException();

            // 不要忘记在MahuaModule中注册
        }
    }
}
