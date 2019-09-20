using Newbe.Mahua.MahuaEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 11:05:17
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupJoiningInvitationReceivedMahuaEvent : IGroupJoiningInvitationReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GroupJoiningInvitationReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessJoinGroupRequest(GroupJoiningRequestReceivedContext context)
        {
            // 自动同意入群邀请
            _mahuaApi.AcceptGroupJoiningInvitation(context.GroupJoiningRequestId, context.ToGroup, context.FromQq);
        }
    }
}
