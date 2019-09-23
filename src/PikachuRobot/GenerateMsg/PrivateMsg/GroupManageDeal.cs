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
    /// @since : 2019/9/23 15:49:10
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupManageDeal : IPrivateMsgDeal
    {
        private PikachuDataContext _dbContext;

        public GroupManageDeal(PikachuDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            Match match;

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*查看群授权[\s|\n|\r]*[\s|\n|\r]*$")).Success)
            {
                StringBuilder builder = new StringBuilder();

                var list = _dbContext.GroupAuths.Where(u => u.Enable).OrderByDescending(u => u.UpdateTime)
                    .ThenByDescending(u => u.CreateTime).ToList();

                if (list.Count == 0)
                {
                    return "暂无授权";
                }

                for (int i = 0; i < list.Count; i++)
                {
                    builder.Append($"{(i + 1).ToString()}.{list[i].GroupNo}");
                    builder.AppendLine();
                }

                return builder.ToString();
            }
            else if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*添加群授权[\s|\n|\r]*(\d*)[\s|\n|\r]*$")).Success)
            {
                var info = match.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(info))
                {
                    return AddGroupAuth(info);
                }
            }
            else if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*取消群授权[\s|\n|\r]*(\d*)[\s|\n|\r]*$")).Success)
            {
                var info = match.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(info))
                {
                    return RemoveGroupAuth(info);
                }
            }

            return String.Empty;
        }

        public string RemoveGroupAuth(string groupNo)
        {
            var old = _dbContext.GroupAuths.FirstOrDefault(u => u.GroupNo.Equals(groupNo));

            if (old != null)
            {
                old.Enable = false;
                old.UpdateTime = DateTime.Now;
                _dbContext.SaveChanges();
            }

            return "取消授权成功!";
        }

        public string AddGroupAuth(string groupNo)
        {
            var old = _dbContext.GroupAuths.FirstOrDefault(u => u.GroupNo.Equals(groupNo));

            if (old != null)
            {
                old.Enable = true;
                old.UpdateTime = DateTime.Now;
            }
            else
            {
                _dbContext.GroupAuths.Add(new GroupAuth()
                {
                    GroupNo = groupNo,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Enable = true
                });
                ;
            }

            _dbContext.SaveChanges();

            return "添加授权成功!";
        }
    }
}