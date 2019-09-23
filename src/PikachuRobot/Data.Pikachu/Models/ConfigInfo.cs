using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Models
{
    /// <summary>
    /// @desc : 配置信息  
    /// @author : monster_yj
    /// @create : 2019/9/22 16:25:36 
    /// @source : 
    /// </summary>
    public class ConfigInfo:BaseModel<int>
    {

        /// <summary>
        /// 查找key
        /// </summary>
        [Required]
        public string Key { get; set; }

        /// <summary>
        /// 输出值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DefaultValue(false)]
        public bool Enable { get; set; }

    }
}