using IServiceSupply;
using Services.PikachuSystem;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenerateMsg.PrivateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 16:40:05
    /// @source : 
    /// @des : 群转载
    /// </summary>
    public class GroupMsgCopyDeal : IGeneratePrivateMsgDeal
    {
        public GroupMsgCopyDeal(GroupMsgCopyService groupMsgCopyService)
        {
            GroupMsgCopyService = groupMsgCopyService;
        }

        private GroupMsgCopyService GroupMsgCopyService { get; }

        public async Task<PrivateRes> Run(string msg, string account, Lazy<string> getLoginAccount)
        {
            Match match;

            if ("查看群转载".Equals(msg))
            {
                StringBuilder builder = new StringBuilder();

                var list = await GroupMsgCopyService.GetByAccount(getLoginAccount.Value).OrderByDescending(u => u.Id)
                    .ToListAsync();

                if (list.Count == 0)
                {
                    return "暂无";
                }

                builder.AppendLine(" 来源群->目标群");

                for (int i = 0; i < list.Count; i++)
                {
                    builder.AppendLine($"{(i + 1).ToString()}.{list[i].FromGroup}->{list[i].TargetGroup}");
                }

                builder.AppendLine();
                builder.AppendLine("可使用 [添加群转载] [来源群]->[目标群] 添加配置");
                builder.AppendLine("示例：添加群转载 10086->10010");

                return builder.ToString();
            }

            if ((match = Regex.Match(msg, @"^添加群转载[\s|\n|\r]*(\d*)->(\d*)$")).Success)
            {
                var fromGroup = match.Groups[1].Value;
                var targetGroup = match.Groups[2].Value;
                if (!(string.IsNullOrWhiteSpace(fromGroup) || string.IsNullOrWhiteSpace(targetGroup)))
                {
                    await GroupMsgCopyService.AddGroupCopyAsync(fromGroup, targetGroup, getLoginAccount.Value);

                    var builder = new StringBuilder();

                    builder.AppendLine("添加成功！");
                    builder.AppendLine();
                    builder.AppendLine("可使用 [删除群转载] [来源群]->[目标群] 删除配置");
                    builder.AppendLine("示例：删除群转载 10086->10010");

                    return builder.ToString();
                }

                return null;
            }

            if ((match = Regex.Match(msg, @"^删除群转载[\s|\n|\r]*(\d*)->(\d*)$")).Success)
            {
                var fromGroup = match.Groups[1].Value;
                var targetGroup = match.Groups[2].Value;
                if (!(string.IsNullOrWhiteSpace(fromGroup) || string.IsNullOrWhiteSpace(targetGroup)))
                {
                    await GroupMsgCopyService.RemoveGroupCopyAsync(fromGroup, targetGroup, getLoginAccount.Value);

                    return "删除转载成功!";
                }
            }

            return null;
        }
    }
}