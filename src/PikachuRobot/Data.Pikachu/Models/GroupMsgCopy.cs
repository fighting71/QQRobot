using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 16:38:11
    /// @source : 
    /// @des : 群消息转载
    /// </summary>
    public class GroupMsgCopy
    {

        public int Id { get; set; }

        /// <summary>
        /// 来源群
        /// </summary>
        public string FromGroup { get; set; }

        /// <summary>
        /// 目标群
        /// </summary>
        public string TargetGroup { get; set; }

        /// <summary>
        /// 处理人账号
        /// </summary>
        public string Person { get; set; }

    }
}
