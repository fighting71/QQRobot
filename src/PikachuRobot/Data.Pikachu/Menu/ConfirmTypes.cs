using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Menu
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/15 16:04:15
    /// @source : 
    /// @des : 
    /// </summary>
    public enum ConfirmTypes : byte
    {
        /// <summary>
        /// 好友提醒
        /// </summary>
        FriendConfirm = 1,
        /// <summary>
        /// 群组提醒
        /// </summary>
        GroupConfirm = 2,
        /// <summary>
        /// 公告提醒
        /// </summary>
        NoticeConfirm = 3
        
    }
}
