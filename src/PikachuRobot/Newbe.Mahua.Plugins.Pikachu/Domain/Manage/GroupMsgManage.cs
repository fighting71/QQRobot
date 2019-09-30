using IServiceSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.Mahua.MahuaEvents;
using NLog;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 14:44:54
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupMsgManage : BaseList<Func<GenerateGroupMsgDel>>, IGenerateGroupMsgDeal
    {
        public async Task<GroupRes> Run(string msg, string account, string groupNo, Lazy<string> getLoginAccount)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var res = await list[i]()(msg, account, groupNo, getLoginAccount);
                if (res == null) continue;
                return res.Success ? res : null;
            }

            return null;
        }
    }
}