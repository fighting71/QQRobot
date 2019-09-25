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
    /// @since : 2019/9/25 14:05:16
    /// @source : 
    /// @des : 群设置配置
    /// </summary>
    public class GroupConfig : BaseModel<int>
    {

        /// <summary>
        /// 处理人
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 配置类型
        /// </summary>
        public GroupConfigTypes GetGroupConfigType { get; set; }

        /// <summary>
        /// 配置群
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enable { get; set; }

    }
}
