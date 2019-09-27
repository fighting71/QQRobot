using System;
using System.Text.RegularExpressions;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using NLog;

namespace GenerateMsg
{
    public class NoticeDeal: IGroupMsgDeal
    {
        private static Logger _logger = LogManager.GetLogger(nameof(NoticeDeal));
        
        public GroupRes Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            if (Regex.IsMatch(context.Message, @"[\s|\n|\r]*公告设置[\s|\n|\r]*"))
            {
                return "请输入Cron表达式 参考:https://www.cnblogs.com/knowledgesea/p/4705796.html";
            }

            return null;

        }
    }
}