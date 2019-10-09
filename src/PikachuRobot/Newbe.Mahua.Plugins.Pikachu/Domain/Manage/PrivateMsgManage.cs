using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateMsg.PrivateMsg;
using IServiceSupply;
using Newbe.Mahua.MahuaEvents;
using NLog;
using Services.PikachuSystem;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 13:46:23
    /// @source : 
    /// @des : 
    /// </summary>
    public class PrivateMsgManage : BaseList<GeneratePrivateMsgDel>, IGeneratePrivateMsgDeal
    {

        public PrivateMsgManage(ConfigCacheDeal configCacheDeal,ConfigDeal configDeal,GroupAuthDeal groupAuthDeal, GroupMsgCopyDeal groupMsgCopyDeal,
            ConfigService configService)
        {
            this.AddDeal(configCacheDeal.Run)
                .AddDeal(configDeal.Run)
                .AddDeal(groupAuthDeal.Run)
                .AddDeal(groupMsgCopyDeal.Run)
                .AddDeal(async (context, api, getLoginQq) =>
                {
                    var info = await configService.Get("Private.Confirm.Default");
                    return info?.Value;
                });
        }
        
        public async Task<PrivateRes> Run(string msg, string account, Lazy<string> getLoginAccount)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var res = await list[i](msg, account, getLoginAccount);
                if (res == null) continue;
                return res.Success ? res : null;
            }

            return null;
        }
    }
}