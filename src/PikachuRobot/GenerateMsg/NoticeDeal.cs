using System;
using System.Text.RegularExpressions;
using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using NLog;

namespace GenerateMsg
{
    public class NoticeDeal: IGenerateMsg
    {
        private static Logger _logger = LogManager.GetLogger(nameof(NoticeDeal));
        
        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            if (!Regex.IsMatch(context.Message, @"[\s|\n|\r]*公告设置[\s|\n|\r]*"))
            {
                return string.Empty;
            }
            
            // 添加标记
            var groupMemebers = mahuaApi.GetGroupMemebers(context.FromGroup);
            try
            {
                var groupMemebersWithModel = mahuaApi.GetGroupMemebersWithModel(context.FromGroup);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

            GroupMemberInfo groupMemberInfo = null;
            
            try
            {
                groupMemberInfo = mahuaApi.GetGroupMemberInfo(context.FromGroup, context.FromQq);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

            if (groupMemberInfo == null) return string.Empty;
            
            if (groupMemberInfo.Authority == GroupMemberAuthority.Leader || groupMemberInfo.Authority == GroupMemberAuthority.Manager)
            {
                // 追加回复标记，记录操作人
                return "请输入Cron表达式 参考:https://www.cnblogs.com/knowledgesea/p/4705796.html";
            }
            else
            {
                return "仅管理员或群主有权使用此权限";
            }
            
        }
    }
}