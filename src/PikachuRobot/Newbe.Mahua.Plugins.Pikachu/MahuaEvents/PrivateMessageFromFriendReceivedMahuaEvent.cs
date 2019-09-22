using Commond.Tools;
using Data.Pikachu;
using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua.Plugins.Pikachu.ApiExtension;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Pikachu.Models;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaEvents
{
    /// <summary>
    /// 来自好友的私聊消息接收事件
    /// </summary>
    public class PrivateMessageFromFriendReceivedMahuaEvent
        : IPrivateMessageFromFriendReceivedMahuaEvent
    {

        private readonly IMahuaApi _mahuaApi;

        private static Dictionary<string, string> _dictionary = new Dictionary<string, string>();

        public PrivateMessageFromFriendReceivedMahuaEvent(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessFriendMessage(PrivateMessageFromFriendReceivedContext context)
        {
            PikachuDataContext dbContext = InstanceFactory.Get<PikachuDataContext>();

            if (dbContext.managers.FirstOrDefault(u => u.Enable && u.Account.Equals(context.FromQq)) == null) // 非管理员
            {
                return;
            }

            if (_dictionary.ContainsKey($"{context.FromQq}_config_set"))
            {
                var opt = _dictionary[$"{context.FromQq}_config_set"];


                if (opt.Equals("remove"))
                {
                    var search =
                        dbContext.configInfos.FirstOrDefault(u => u.Enable && u.Key.Equals(context.Message.Trim()));
                    if (search != null)
                    {
                        search.Enable = false;
                        dbContext.SaveChanges();
                    }
                    _mahuaApi.SendPrivateMessage(context.FromQq)
                        .Text("\t禁用成功")
                        .Done();
                }
                else if (opt.Equals("add"))
                {
                    var info = context.Message.Split('|');
                    if (info.Length == 3)
                    {
                        if (!string.IsNullOrWhiteSpace(info[0]))
                        {
                            var config = new ConfigInfo()
                            {
                                CreateTime = DateTime.Now,
                                Key = info[0],
                                Value = info[1],
                                Description = info[2],
                                Enable = true
                            };

                            var old = dbContext.configInfos.FirstOrDefault(u =>
                                u.Enable && u.Key.Equals(config.Key, StringComparison.CurrentCultureIgnoreCase));

                            if (old != null)
                            {
                                old.Value = config.Value;
                                old.UpdateTime = DateTime.Now;
                            }
                            else
                            {
                                config.CreateTime = DateTime.Now;
                                dbContext.configInfos.Add(config);
                            }

                            dbContext.SaveChanges();

                            _mahuaApi.SendPrivateMessage(context.FromQq)
                                .Text("\t添加成功！")
                                .Done();
                        }
                        else
                        {
                            _mahuaApi.SendPrivateMessage(context.FromQq)
                                .Text("\t配置key不能为空！")
                                .Done();
                        }
                    }
                    else
                    {
                        _mahuaApi.SendPrivateMessage(context.FromQq)
                            .Text("\t输入格式有误！")
                            .Done();
                    }
                }

                _dictionary.Remove($"{context.FromQq}_config_set");

                return;
            }


            if (string.IsNullOrWhiteSpace(context.Message))
            {
                _mahuaApi.SendPrivateMessage(context.FromQq)
                    .Text($"欢迎使用{ConfigConst.AppName} 系统")
                    .Newline()
                    .Text("\t试试对我说 [配置管理] 吧")
                    .Done();
                return;
            }

            if (Regex.IsMatch(context.Message, @"[\s|\n|\r]*配置管理[\s|\n|\r]*"))
            {
                _mahuaApi.SendPrivateMessage(context.FromQq)
                    .Text(@"当前配置管理支出内容:
[查看列表] [添加配置] [禁用配置]
")
                    .Done();
                return;
            }

            if (Regex.IsMatch(context.Message, @"[\s|\n|\r]*查看列表[\s|\n|\r]*"))
            {
                var list = dbContext.configInfos.Where(u => u.Enable).OrderByDescending(u => u.UpdateTime)
                    .ThenByDescending(u => u.CreateTime).ToList();

                StringBuilder builder = new StringBuilder();

                builder.Append(
                    $"\t [配置key] - [配置value] [配置描述]");

                for (int i = 0; i < list.Count(); i++)
                {
                    builder.Append(
                        $"{(i + 1)}. {list[i].Key} - {list[i].Value} [{list[i].Description}]");
                    builder.AppendLine();
                }

                _mahuaApi.SendPrivateMessage(context.FromQq).Text(builder.ToString()).Done();

                return;
            }

            if (Regex.IsMatch(context.Message, @"[\s|\n|\r]*添加配置[\s|\n|\r]*"))
            {
                // 添加标记
                _dictionary.Add($"{context.FromQq}_config_set", "add");

                _mahuaApi.SendPrivateMessage(context.FromQq)
                    .Text("请按照此格式填写你要添加的配置:[配置key]|[配置value]|[配置描述](请注意内容中不要使用'|')").Done();

                return;
            }

            if (Regex.IsMatch(context.Message, @"[\s|\n|\r]*禁用配置[\s|\n|\r]*"))
            {
                // 添加标记
                _dictionary.Add($"{context.FromQq}_config_set", "remove");

                _mahuaApi.SendPrivateMessage(context.FromQq).Text("请输入你要删除的'配置key'").Done();

                return;
            }

            // todo 填充处理逻辑
            //throw new NotImplementedException();

            // 不要忘记在MahuaModule中注册

            //if (string.IsNullOrWhiteSpace(context.Message))
            //{
            //    _mahuaApi
            //        .SendPrivateMessage(context.FromQq)
            //        .Text(ConfigConst.DefaultPrivateMsg)
            //        .Done();
            //    //_mahuaApi.SendPrivateMessage(context.FromQq).SendDefaultPrivateMsg().Done();
            //    return;
            //}

            //// 戳一戳
            //_mahuaApi.SendPrivateMessage(context.FromQq)
            //    .Shake()
            //    .Done();

            //_mahuaApi.SendPrivateMessage(context.FromQq)
            //.Text(context.Message)
            //.Done();
        }
    }
}