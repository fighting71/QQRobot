using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Services.PikachuSystem;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GenerateMsg.PrivateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 16:40:05
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupMsgCopyDeal : IPrivateMsgDeal
    {

        public GroupMsgCopyDeal(GroupMsgCopyService groupMsgCopyService)
        {
            GroupMsgCopyService = groupMsgCopyService;
        }

        public GroupMsgCopyService GroupMsgCopyService { get; }

        public PrivateRes Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            Match match;

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*查看群转载[\s|\n|\r]*[\s|\n|\r]*$")).Success)
            {
                StringBuilder builder = new StringBuilder();

                var list = GroupMsgCopyService.GetByAccount(mahuaApi.GetLoginQq()).OrderByDescending(u => u.Id).ToList();

                if (list.Count == 0)
                {
                    return "暂无";
                }

                builder.AppendLine($" 来源群->目标群");

                for (int i = 0; i < list.Count; i++)
                {
                    builder.AppendLine($"{(i + 1).ToString()}.{list[i].FromGroup}->{list[i].TargetGroup}");
                }

                builder.AppendLine();
                builder.AppendLine("可使用 [添加群转载] [来源群]->[目标群] 添加配置");
                builder.AppendLine("示例：添加群转载 10086->10010");

                return builder.ToString();
            }
            else if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*添加群转载[\s|\n|\r]*(\d*)->(\d*)[\s|\n|\r]*$")).Success)
            {
                var fromGroup = match.Groups[1].Value;
                var targetGroup = match.Groups[2].Value;
                if (!(string.IsNullOrWhiteSpace(fromGroup) || string.IsNullOrWhiteSpace(targetGroup)))
                {
                    GroupMsgCopyService.AddGroupAuth(fromGroup, targetGroup, mahuaApi.GetLoginQq(),out var msg);

                    var builder = new StringBuilder(msg);

                    builder.AppendLine();
                    builder.AppendLine();
                    builder.AppendLine("可使用 [删除群转载] [来源群]->[目标群] 删除配置");
                    builder.AppendLine("示例：删除群转载 10086->10010");

                    return builder.ToString();
                }
            }
            else if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*删除群转载[\s|\n|\r]*(\d*)->(\d*)[\s|\n|\r]*$")).Success)
            {
                var fromGroup = match.Groups[1].Value;
                var targetGroup = match.Groups[2].Value;
                if (!(string.IsNullOrWhiteSpace(fromGroup) || string.IsNullOrWhiteSpace(targetGroup)))
                {
                    GroupMsgCopyService.RemoveGroupAuth(fromGroup, targetGroup, mahuaApi.GetLoginQq(),out var msg);

                    return msg;

                }
            }

            return null;
        }

    }
}
