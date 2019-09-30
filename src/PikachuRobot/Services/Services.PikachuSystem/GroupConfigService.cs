using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu;
using Data.Pikachu.Menu;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 14:34:26
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupConfigService : BaseService
    {
        public GroupConfigService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        /// <summary>
        /// 添加配置 [单值]
        /// </summary>
        /// <param name="group"></param>
        /// <param name="account"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int AddSingleInfo(string group, string account, string info, GroupConfigTypes type)
        {
            var old = PikachuDataContext.GroupConfigs.FirstOrDefault(u =>
                u.Enable && u.GetGroupConfigType == type && u.Group.Equals(group) && u.Account.Equals(account));

            if (old != null)
            {
                old.Value = info;
                old.UpdateTime = DateTime.Now;
            }
            else
            {
                PikachuDataContext.GroupConfigs.Add(new Data.Pikachu.Models.GroupConfig()
                {
                    Account = account,
                    Group = group,
                    Enable = true,
                    Value = info,
                    GetGroupConfigType = type,
                });
            }

            return PikachuDataContext.SaveChanges();
        }

        public int RemoveConfig(string group, string account, GroupConfigTypes type)
        {
            return PikachuDataContext.Database.ExecuteSqlCommand("UPDATE ");
        }


        /// <summary>
        /// 添加配置 [单值]
        /// </summary>
        /// <param name="group"></param>
        /// <param name="account"></param>
        /// <param name="info"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<int> AddSingleInfoAsync(string group, string account, string info, GroupConfigTypes type)
        {
            var old = await PikachuDataContext.GroupConfigs.FirstOrDefaultAsync(u =>
                u.Enable && u.GetGroupConfigType == type && u.Group.Equals(group) && u.Account.Equals(account));

            if (old != null)
            {
                old.Value = info;
                old.UpdateTime = DateTime.Now;
            }
            else
            {
                PikachuDataContext.GroupConfigs.Add(new Data.Pikachu.Models.GroupConfig()
                {
                    Account = account,
                    Group = group,
                    Enable = true,
                    Value = info,
                    GetGroupConfigType = type,
                });
            }

            return await PikachuDataContext.SaveChangesAsync();
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="group"></param>
        /// <param name="account"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<int> RemoveConfigAsync(string group, string account, GroupConfigTypes type)
        {
            return await PikachuDataContext.Database.ExecuteSqlCommandAsync($@"
UPDATE groupconfigs
	SET
		Enable=0
	WHERE Account = {account} AND Group = {group} AND Enable = 1 AND GetGroupConfigType = {(int) type}
");
        }
    }
}