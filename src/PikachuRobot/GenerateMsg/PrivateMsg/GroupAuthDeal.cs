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
    /// @since : 2019/9/23 15:49:10
    /// @source : 
    /// @des : 群授权
    /// </summary>
    public class GroupAuthDeal : IGeneratePrivateMsgDeal
    {
        public GroupAuthDeal(GroupAuthService groupManageService)
        {
            GroupManageService = groupManageService;
        }

        private GroupAuthService GroupManageService { get; }

        public async Task<PrivateRes> Run(string msg, string account, Lazy<string> getLoginAccount)
        {
            if ("查看群授权".Equals(msg))
            {
                StringBuilder builder = new StringBuilder();

                var list = await GroupManageService.GetAll().OrderByDescending(u => u.UpdateTime)
                    .ThenByDescending(u => u.CreateTime).ToListAsync();

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

            Match match;

            if ((match = Regex.Match(msg, @"^添加群授权[\s|\n|\r]*(\d*)$")).Success)
            {
                var info = match.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(info))
                {
                    await GroupManageService.AddGroupAuthAsync(info);

                    StringBuilder builder = new StringBuilder();

                    builder.AppendLine("添加授权成功!");
                    builder.AppendLine();
                    builder.AppendLine("可使用 [取消群授权] [群号] 来取消授权");
                    builder.AppendLine("示例：取消群授权 10086");

                    return builder.ToString();
                }

                return null;
            }

            if ((match = Regex.Match(msg, @"^取消群授权[\s|\n|\r]*(\d*)$")).Success)
            {
                var info = match.Groups[1].Value;

                if (!string.IsNullOrWhiteSpace(info))
                {
                    await GroupManageService.RemoveGroupAuthAsync(info);
                    return "取消授权成功!";
                }
            }

            return null;
        }
    }
}