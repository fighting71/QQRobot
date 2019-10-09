using IServiceSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu.Menu;
using GenerateMsg.GroupMsg;
using Newbe.Mahua.MahuaEvents;
using NLog;
using Services.PikachuSystem;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 14:44:54
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupMsgManage : BaseList<GenerateGroupMsgDel>, IGenerateGroupMsgDeal
    {

        public GroupMsgManage(AddPetCacheDeal addPetCacheDeal, GroupConfigDeal groupConfigDeal,
            IdiomsSolitaireCacheDeal idiomsSolitaireCacheDeal,
            IdiomsSolitaireDeal idiomsSolitaireDeal, MemberAmountDeal memberAmountDeal, PetDeal petDeal,
            SignDeal signDeal,GroupConfigService groupConfigService)
        {
            this.AddDeal(addPetCacheDeal.Run)
                .AddDeal(idiomsSolitaireCacheDeal.Run)
                .AddDeal(idiomsSolitaireDeal.Run)
                .AddDeal(groupConfigDeal.Run)
                .AddDeal(memberAmountDeal.Run)
                .AddDeal(petDeal.Run)
                .AddDeal(signDeal.Run)
                .AddDeal(async (msg, account, groupNo, getLoginAccount) =>
                    {
                        var loginQq = getLoginAccount.Value;

                        var info = await groupConfigService.GetSingle(loginQq, groupNo, GroupConfigTypes.DefaultConfirm);

                        return info?.Value;
                    }
                );
        }
        
        public async Task<GroupRes> Run(string msg, string account, string groupNo, Lazy<string> getLoginAccount)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var res = await list[i](msg, account, groupNo, getLoginAccount);
                if (res == null) continue;
                return res.Success ? res : null;
            }

            return null;
        }
    }
}