using IServiceSupply;
using Services.PikachuSystem;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Data.Pikachu.Menu;

namespace GenerateMsg.GroupMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 14:11:38
    /// @source : 
    /// @des : 群配置
    /// </summary>
    public class GroupConfigDeal : IGenerateGroupMsgDeal
    {
        private readonly ManageService _manageService;
        private readonly GroupConfigService _groupConfigService;

        public GroupConfigDeal(ManageService manageService, GroupConfigService groupConfigService)
        {
            this._manageService = manageService;
            this._groupConfigService = groupConfigService;
        }

        public async Task<GroupRes> Run(string msg, string account, string groupNo, Lazy<string> getLoginAccount)
        {
            // 非管理员无权访问
            if (!await _manageService.IsManageAsync(account)) return null;

            Match match;

            if ((match = Regex.Match(msg, @"^设置入群提示[\s|\n|\r]([\s|\S]*)")).Success)
            {
                var info = match.Groups[1].Value;

                if (await AddInfo(info, groupNo, getLoginAccount.Value, GroupConfigTypes.JoinConfirm))
                {
                    return "设置成功！";
                }

                return null;
            }

            if ((match = Regex.Match(msg, @"^设置默认提示[\s|\n|\r]([\s|\S]*)")).Success)
            {
                var info = match.Groups[1].Value;

                if (await AddInfo(info, groupNo, getLoginAccount.Value, GroupConfigTypes.DefaultConfirm))
                {
                    return "设置成功！";
                }

                return null;
            }


            if ((match = Regex.Match(msg, @"^设置退群提示[\s|\n|\r]([\s|\S]*)")).Success)
            {
                var info = match.Groups[1].Value;

                if (await AddInfo(info, groupNo, getLoginAccount.Value, GroupConfigTypes.LeaveConfirm))
                {
                    return "设置成功！";
                }

                return null;
            }

            if ("删除退群提示".Equals(msg))
            {
                if (await RemoveInfo(groupNo, getLoginAccount.Value, GroupConfigTypes.LeaveConfirm))
                {
                    return "删除成功!";
                }

                return null;
            }

            if ("删除入群提示".Equals(msg))
            {
                if (await RemoveInfo(groupNo, getLoginAccount.Value, GroupConfigTypes.JoinConfirm))
                {
                    return "删除成功!";
                }

                return null;
            }

            if ("删除默认提示".Equals(msg))
            {
                if (await RemoveInfo(groupNo, getLoginAccount.Value, GroupConfigTypes.DefaultConfirm))
                {
                    return "删除成功!";
                }

                return null;
            }

            return null;
        }

        private async Task<bool> RemoveInfo(string groupNo, string account, GroupConfigTypes type)
        {
            if (await _groupConfigService.RemoveConfigAsync(groupNo, account, type) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> AddInfo(string info, string groupNo, string account, GroupConfigTypes type)
        {
            if (!string.IsNullOrWhiteSpace(info)
                && await _groupConfigService.AddSingleInfoAsync(groupNo, account, info, type) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}