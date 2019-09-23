using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 15:44:16
    /// @source : 
    /// @des : 群聊授权
    /// </summary>
    public class GroupAuth:BaseModel<int>
    {

        /// <summary>
        /// 群号
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 级别【备用】
        /// </summary>
        public int Level { get; set; }

    }
}
