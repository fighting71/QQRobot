using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Models
{
    /// <summary>
    /// @desc : Manager  
    /// @author : monster_yj
    /// @create : 2019/9/22 17:40:13 
    /// @source : 
    /// </summary>
    public class Manager : BaseModel<int>
    {

        /// <summary>
        /// 账号
        /// </summary>
        [Required]
        public string Account { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DefaultValue(false)]
        public bool Enable { get; set; }

    }
}
