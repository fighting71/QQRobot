using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua.Plugins.Pikachu.MahuaEvents;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/14 10:42:57
    /// @source : 
    /// @des : 通用事件注册
    /// </summary>
    public class CommandEventModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // 将需要监听的事件注册，若缺少此注册，则不会调用相关的实现类
            builder.RegisterType<PrivateMessageFromFriendReceivedMahuaEvent>()
                .As<IPrivateMessageFromFriendReceivedMahuaEvent>();

            builder.RegisterType<GroupMessageReceivedMahuaEvent>()
                .As<IGroupMessageReceivedMahuaEvent>();

            builder.RegisterType<MahuaMenuClickedMahuaEvent>()
                .As<IMahuaMenuClickedMahuaEvent>();

            builder.RegisterType<GroupMemberIncreasedMahuaEvent>()
                .As<IGroupMemberIncreasedMahuaEvent>();

            builder.RegisterType<GroupMemberDecreasedMahuaEvent>()
                .As<IGroupMemberDecreasedMahuaEvent>();

            // 无法处理...
            //builder.RegisterType<FriendAddingRequestMahuaEvent>()
            //.As<IFriendAddingRequestMahuaEvent>();

            // 无法处理...
            //builder.RegisterType<GroupJoiningInvitationReceivedMahuaEvent>()
            //    .As<IGroupJoiningInvitationReceivedMahuaEvent>();

        }
    }
}
