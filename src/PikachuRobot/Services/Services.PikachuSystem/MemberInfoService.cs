using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu;
using Data.Pikachu.Models;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 14:33:56
    /// @source : 
    /// @des : 
    /// </summary>
    public class MemberInfoService : BaseService
    {
        public MemberInfoService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        public int ChangeAmount(string group, string account,decimal amount)
        {
            var memberInfo = PikachuDataContext.MemberInfos.FirstOrDefault(u => u.Enable && u.Group == group && u.Account == account);
            if (memberInfo != null)
            {
                memberInfo.Amount += amount;
                return PikachuDataContext.SaveChanges();
            }
            return 0;
        }

        /// <summary>
        /// 获取成员信息
        /// </summary>
        /// <param name="group">群号</param>
        /// <param name="account">成员账号</param>
        /// <returns></returns>
        public MemberInfo GetInfo(string group, string account)
        {
            var memberInfo = PikachuDataContext.MemberInfos.FirstOrDefault(u => u.Enable && u.Group == group && u.Account == account);

            if (memberInfo == null)
            {

                var list = PikachuDataContext.BillFlows.Where(u => u.Enable && u.Group == group && u.Account == account);

                decimal amount = list.Where(u => u.Enable && u.BillType == Data.Pikachu.Menu.BillTypes.Sign).Sum(u => u.ActualAmount)
                    - list.Where(u => u.BillType == Data.Pikachu.Menu.BillTypes.Consume).Sum(u => u.ActualAmount);

                memberInfo = new MemberInfo()
                {
                    Account = account,
                    Group = group,
                    Amount = amount,
                };

                PikachuDataContext.MemberInfos.Add(memberInfo);

                PikachuDataContext.SaveChanges();

            }

            return memberInfo;
        }

    }
}
