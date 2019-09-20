using Newbe.Mahua.MahuaEvents;
using System;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 群成员增多事件
    /// </summary>
    public class GroupMemberIncreasedMahuaEvent
        : IGroupMemberIncreasedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public GroupMemberIncreasedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMemberIncreased(GroupMemberIncreasedContext context)
        {

            if (context.JoinedQq.Equals(_mahuaApi.GetLoginQq()))// 排除机器人入群
            {
                return;
            }
            
            _mahuaApi.SendGroupMessage(context.FromGroup)
                .At(context.JoinedQq)
                .Text("欢迎您加入技术交流群，我是Pikachu机器人，艾特我回复“指令”两个字可以为您提供服务哦。")
                .Done();

            // todo 填充处理逻辑
            //throw new NotImplementedException();

            // 不要忘记在MahuaModule中注册
        }
    }
}
