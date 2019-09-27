using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Menu
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 14:07:06
    /// @source : 
    /// @des : 群聊配置
    /// </summary>
    public enum GroupConfigTypes : byte
    {
        /// <summary>
        /// 入群提醒
        /// </summary>
        JoinConfirm = 1,

        /// <summary>
        /// 默认提醒
        /// </summary>
        DefaultConfirm = 2,

        /// <summary>
        /// 退群/飞机票提醒
        /// </summary>
        LeaveConfirm = 3,
    }
}