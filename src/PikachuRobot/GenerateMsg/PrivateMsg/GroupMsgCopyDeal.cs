using Data.Pikachu;
using Data.Pikachu.Models;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;
using System.Collections.Generic;
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
    /// @des : 
    /// </summary>
    public class GroupMsgCopyDeal : IPrivateMsgDeal
    {

        private PikachuDataContext _dbContext;

        public GroupMsgCopyDeal(PikachuDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            Match match;

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*查看群转载[\s|\n|\r]*[\s|\n|\r]*$")).Success)
            {
                StringBuilder builder = new StringBuilder();

                var loginQq = mahuaApi.GetLoginQq();

                var list = _dbContext.GroupMsgCopys.Where(u => u.Person.Equals(loginQq)).OrderByDescending(u => u.Id).ToList();

                if (list.Count == 0)
                {
                    return "暂无";
                }

                builder.Append($" 来源群->目标群");
                builder.AppendLine();

                for (int i = 0; i < list.Count; i++)
                {
                    builder.Append($"{(i + 1).ToString()}.{list[i].FromGroup}->{list[i].TargetGroup}");
                    builder.AppendLine();
                }

                return builder.ToString();
            }
            else if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*添加群转载[\s|\n|\r]*(\d*)->(\d*)[\s|\n|\r]*$")).Success)
            {
                var fromGroup = match.Groups[1].Value;
                var targetGroup = match.Groups[2].Value;
                if (!(string.IsNullOrWhiteSpace(fromGroup) || string.IsNullOrWhiteSpace(targetGroup)))
                {
                    return AddGroupAuth(fromGroup, targetGroup, mahuaApi.GetLoginQq());
                }
            }
            else if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*删除群转载[\s|\n|\r]*(\d*)->(\d*)[\s|\n|\r]*$")).Success)
            {
                var fromGroup = match.Groups[1].Value;
                var targetGroup = match.Groups[2].Value;
                if (!(string.IsNullOrWhiteSpace(fromGroup) || string.IsNullOrWhiteSpace(targetGroup)))
                {
                    return RemoveGroupAuth(fromGroup, targetGroup, mahuaApi.GetLoginQq());
                }
            }

            return String.Empty;
        }


        public string RemoveGroupAuth(string fromGroup, string targetGroup, string dealPerson)
        {
            var old = _dbContext.GroupMsgCopys.FirstOrDefault(u => u.Person.Equals(dealPerson) && u.FromGroup.Equals(fromGroup) && u.TargetGroup.Equals(targetGroup));

            _dbContext.Entry(old).State = System.Data.Entity.EntityState.Deleted;

            _dbContext.SaveChanges();

            return "删除转载成功!";
        }

        public string AddGroupAuth(string fromGroup, string targetGroup, string dealPerson)
        {
            var old = _dbContext.GroupMsgCopys.FirstOrDefault(u => u.Person.Equals(dealPerson) && u.FromGroup.Equals(fromGroup) && u.TargetGroup.Equals(targetGroup));

            if (old != null)
            {
                return "已存在转载设置";
            }
            else
            {
                _dbContext.GroupMsgCopys.Add(new GroupMsgCopy()
                {
                    FromGroup = fromGroup,
                    TargetGroup = targetGroup,
                    Person = dealPerson
                });
                ;
            }

            _dbContext.SaveChanges();

            return "添加转载设置成功!";
        }

    }
}
