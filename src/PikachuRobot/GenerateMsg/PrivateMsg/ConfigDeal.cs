using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Pikachu;
using Data.Pikachu.Models;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using StackExchange.Redis;

namespace GenerateMsg.PrivateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 10:35:17
    /// @source : 
    /// @des : 
    /// </summary>
    public class ConfigDeal : IPrivateMsgDeal
    {
        private const string AddFlag = "add";
        private const string RemoveFlag = "remove";

        /// <summary>
        /// 有效期
        /// </summary>
        private static TimeSpan Expiry = TimeSpan.FromSeconds(30);

        private IDatabase _database;

        private PikachuDataContext _dbContext;

        public ConfigDeal(IDatabase database, PikachuDataContext dbContext)
        {
            _database = database;
            _dbContext = dbContext;
        }

        public string Run(PrivateMessageFromFriendReceivedContext context, IMahuaApi mahuaApi)
        {
            var key = $"{nameof(ConfigDeal)}_{context.FromQq}";

            // 查看是否存在操作

            var cache = _database.StringGet(key).ToString();

            if (!string.IsNullOrWhiteSpace(cache))
            {
                _database.KeyDelete(key); // 移除key
                if (AddFlag.Equals(cache))
                {
                    return AddInfo(context.Message);
                }

                if (RemoveFlag.Equals(cache))
                {
                    return RemoveKey(context.Message.Trim());
                }
            }

            Match match;
            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*配置管理[\s|\n|\r]*$"))
            {
                return @"当前配置管理支持内容:
[查看配置] [添加配置] [禁用配置]
";
            }

            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*查看配置[\s|\n|\r]*$"))
            {
                var list = _dbContext.ConfigInfos.Where(u => u.Enable).OrderByDescending(u => u.UpdateTime)
                    .ThenByDescending(u => u.CreateTime).ToList();

                if (list.Count == 0) return "暂无配置";

                StringBuilder builder = new StringBuilder();

                builder.Append($"  [配置key]|[配置value]|[配置描述]");
                builder.AppendLine();

                for (int i = 0; i < list.Count(); i++)
                {
                    builder.Append(
                        $"{(i + 1).ToString()}. {list[i].Key}|{list[i].Value}|{list[i].Description}");
                    builder.AppendLine();
                }

                return builder.ToString();
            }

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*添加配置([\s|\S]*)$")).Success)
            {
                var info = match.Groups[1].Value;
                if (string.IsNullOrWhiteSpace(info))
                {
                    // 添加标记
                    if (_database.StringSet(key, AddFlag, Expiry))
                    {
                        return "请按照此格式填写你要添加的配置:[配置key]|[配置value]|[配置描述](请注意内容中不要使用'|')";
                    }

                    return "缓存key失败！";
                }

                return AddInfo(info);
            }

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*禁用配置([\s|\S]*)$")).Success)
            {
                var info = match.Groups[1].Value;
                if (string.IsNullOrWhiteSpace(info))
                {
                    // 添加标记
                    if (_database.StringSet(key, RemoveFlag, Expiry))
                    {
                        return "请输入你要删除的'配置key':";
                    }

                    return "缓存key失败！";
                }

                return RemoveKey(info.Trim());
            }

            return String.Empty;
        }

        protected string RemoveKey(string key)
        {
            var search =
                _dbContext.ConfigInfos.FirstOrDefault(u => u.Enable && u.Key.Equals(key));
            if (search != null)
            {
                search.Enable = false;
                _dbContext.SaveChanges();
            }

            return "删除成功";
        }

        protected string AddInfo(string input)
        {
            var info = input.Split('|');
            if (info.Length == 3)
            {
                if (!string.IsNullOrWhiteSpace(info[0]))
                {
                    var config = new ConfigInfo()
                    {
                        CreateTime = DateTime.Now,
                        Key = info[0].Trim(),
                        Value = info[1],
                        Description = info[2],
                        Enable = true
                    };

                    var old = _dbContext.ConfigInfos.FirstOrDefault(u =>
                        u.Enable && u.Key.Equals(config.Key, StringComparison.CurrentCultureIgnoreCase));

                    if (old != null)
                    {
                        old.Value = config.Value;
                        old.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        config.UpdateTime = DateTime.Now;
                        config.CreateTime = DateTime.Now;
                        _dbContext.ConfigInfos.Add(config);
                    }

                    _dbContext.SaveChanges();

                    return "   添加成功！";
                }
                else
                {
                    return "   配置key不能为空！";
                }
            }
            else
            {
                return "   输入格式有误！";
            }
        }
    }
}