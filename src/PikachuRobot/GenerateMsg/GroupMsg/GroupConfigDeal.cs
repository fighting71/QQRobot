using IServiceSupply;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using Services.PikachuSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 14:11:38
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupConfigDeal : IGroupMsgDeal
    {
        private readonly ManageService manageService;
        private readonly GroupConfigService groupConfigService;

        public GroupConfigDeal(ManageService manageService, GroupConfigService groupConfigService)
        {
            this.manageService = manageService;
            this.groupConfigService = groupConfigService;
        }

        public GroupRes Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {

            // 非管理员无权访问
            if (!manageService.IsManage(context.FromQq)) return null;

            Match match;

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*设置入群提示[\s|\n|\r]([\s|\S]*)")).Success)
            {
                var info = match.Groups[1].Value;

                if (!string.IsNullOrWhiteSpace(info) 
                    && groupConfigService.AddSingleInfo(context.FromGroup, mahuaApi.GetLoginQq(), info, Data.Pikachu.Menu.GroupConfigTypes.JoinConfirm) > 0)
                {
                    return "设置成功！";
                }

            }

            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*设置默认提示[\s|\n|\r]([\s|\S]*)")).Success)
            {
                var info = match.Groups[1].Value;

                if (!string.IsNullOrWhiteSpace(info)
                    && groupConfigService.AddSingleInfo(context.FromGroup, mahuaApi.GetLoginQq(), info, Data.Pikachu.Menu.GroupConfigTypes.DefaultConfirm) > 0)
                {
                    return "设置成功！";
                }

            }


            if ((match = Regex.Match(context.Message, @"^[\s|\n|\r]*设置退群提示[\s|\n|\r]([\s|\S]*)")).Success)
            {
                var info = match.Groups[1].Value;

                if (!string.IsNullOrWhiteSpace(info)
                    && groupConfigService.AddSingleInfo(context.FromGroup, mahuaApi.GetLoginQq(), info, Data.Pikachu.Menu.GroupConfigTypes.LeaveConfirm) > 0)
                {
                    return "设置成功！";
                }

            }

            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*删除退群提示[\s|\n|\r]*$"))
            {
                groupConfigService.RemoveConfig(context.FromGroup, mahuaApi.GetLoginQq(), Data.Pikachu.Menu.GroupConfigTypes.LeaveConfirm);
                return "删除成功!";
            }
            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*删除入群提示[\s|\n|\r]*$"))
            {
                groupConfigService.RemoveConfig(context.FromGroup, mahuaApi.GetLoginQq(), Data.Pikachu.Menu.GroupConfigTypes.LeaveConfirm);
                return "删除成功!";
            }
            if (Regex.IsMatch(context.Message, @"^[\s|\n|\r]*删除默认提示[\s|\n|\r]*$"))
            {
                groupConfigService.RemoveConfig(context.FromGroup, mahuaApi.GetLoginQq(), Data.Pikachu.Menu.GroupConfigTypes.LeaveConfirm);
                return "删除成功!";
            }

            return null;
        }

    }
}
