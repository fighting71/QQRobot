using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 14:27:10
    /// @source : 
    /// @des : 
    /// </summary>
    public class ManageService : BaseService
    {
        public ManageService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool IsManage(string account)
        {
            return PikachuDataContext.Managers.Any(u => u.Enable && u.Account.Equals(account));
        }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> IsManageAsync(string account)
        {
            return await PikachuDataContext.Managers.AnyAsync(u => u.Enable && u.Account.Equals(account));
        }
        
    }
}
