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
    /// @since : 2019/9/23 15:49:10
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupManageDeal : IPrivateMsgDeal
    {

        public GroupManageDeal(GroupManageService groupManageService)
        {
            GroupManageService = groupManageService;
        }

        public GroupManageService GroupManageService { get; }

        public string Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            Match match;

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*查看群授权[\s|\n|\r]*[\s|\n|\r]*$")).Success)
            {
                StringBuilder builder = new StringBuilder();

                var list = GroupManageService.GetAll().OrderByDescending(u => u.UpdateTime)
                    .ThenByDescending(u => u.CreateTime).ToList();

                if (list.Count == 0)
                {
                    return "暂无授权";
                }

                for (int i = 0; i < list.Count; i++)
                {
                    builder.AppendLine($"{(i + 1).ToString()}.{list[i].GroupNo}");
                }

                builder.AppendLine();
                builder.AppendLine("可使用 [添加群授权] [群号] 来添加授权");
                builder.AppendLine("示例：添加群授权 10086");

                return builder.ToString();
            }
            else if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*添加群授权[\s|\n|\r]*(\d*)[\s|\n|\r]*$")).Success)
            {
                var info = match.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(info))
                {

                    GroupManageService.AddGroupAuth(info, out var msg);

                    StringBuilder builder = new StringBuilder(msg);

                    builder.AppendLine();
                    builder.AppendLine();
                    builder.AppendLine("可使用 [取消群授权] [群号] 来取消授权");
                    builder.AppendLine("示例：取消群授权 10086");

                    return builder.ToString();
                }
            }
            else if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*取消群授权[\s|\n|\r]*(\d*)[\s|\n|\r]*$")).Success)
            {
                var info = match.Groups[1].Value;

                if (!string.IsNullOrWhiteSpace(info))
                {
                    GroupManageService.RemoveGroupAuth(info, out var msg);
                    return msg;

                }

            }

            return String.Empty;
        }

       
    }
}