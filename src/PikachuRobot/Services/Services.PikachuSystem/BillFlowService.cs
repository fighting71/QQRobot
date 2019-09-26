using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu;
using Data.Pikachu.Menu;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 14:45:01
    /// @source : 
    /// @des : 
    /// </summary>
    public class BillFlowService : BaseService
    {
        public BillFlowService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        public IQueryable<Data.Pikachu.Models.BillFlow> GetAll()
        {
            return PikachuDataContext.BillFlows.Where(u => u.Enable);
        }

        public int AddBill(string group,string account,decimal amount,decimal actualAmount, BillTypes type,string desc)
        {
            PikachuDataContext.BillFlows.Add(new Data.Pikachu.Models.BillFlow()
             {
                 Account = account,
                 Amount = amount,
                 ActualAmount = actualAmount,
                 BillType = type,
                 Group = group,
                 Description = desc,
                 Enable = true,
             });

            return PikachuDataContext.SaveChanges();

        }

    }
}
