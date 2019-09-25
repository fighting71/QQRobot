using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 16:34:10
    /// @source : 
    /// @des : 成员信息
    /// </summary>
    public class MemberInfo : BaseModel<int>
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
        /// 级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

    }
}
