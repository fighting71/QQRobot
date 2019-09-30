using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IServiceSupply;
using Newbe.Mahua.MahuaEvents;
using NLog;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 13:46:23
    /// @source : 
    /// @des : 
    /// </summary>
    public class PrivateMsgManage : BaseList<Func<GeneratePrivateMsgDel>>, IGeneratePrivateMsgDeal
    {
        public async Task<PrivateRes> Run(string msg, string account, Lazy<string> getLoginAccount)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var res = await list[i]()(msg, account, getLoginAccount);
                if (res == null) continue;
                return res.Success ? res : null;
            }

            return null;
        }
    }
}