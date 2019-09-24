using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.PetSystem.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 11:05:55
    /// @source : 
    /// @des : 用户道具
    /// </summary>
    public class UserProp:BaseModel<int>
    {

        /// <summary>
        /// 道具
        /// </summary>
        public int PropId { get; set; }

        /// <summary>
        /// 道具数量
        /// </summary>
        public long PropNum { get; set; }

        /// <summary>
        /// 所属人
        /// </summary>
        public string Owner { get; set; }

    }
}
