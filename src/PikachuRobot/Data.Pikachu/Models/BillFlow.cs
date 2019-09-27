using Data.Pikachu.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 16:41:31
    /// @source : 
    /// @des : 账单流水
    /// </summary>
    public class BillFlow : BaseModel<int>
    {

        /// <summary>
        /// 所属群
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 成员账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 账单类型
        /// </summary>
        public BillTypes BillType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 扩展字段方便查询
        /// </summary>
        public string Ext { get; set; }

    }
}
