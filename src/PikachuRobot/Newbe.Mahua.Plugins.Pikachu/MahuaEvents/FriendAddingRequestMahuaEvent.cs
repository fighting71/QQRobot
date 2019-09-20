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
    /// @since : 2019/9/20 11:04:42
    /// @source : 
    /// @des : 好友申请接受事件
    /// </summary>
    public class FriendAddingRequestMahuaEvent : IFriendAddingRequestMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public FriendAddingRequestMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessAddingFriendRequest(FriendAddingRequestContext context)
        {
            // 同意好友请求,备注设置为QQ号
            _mahuaApi.AcceptFriendAddingRequest(context.AddingFriendRequestId, context.FromQq, context.FromQq);
        }
    }
}
