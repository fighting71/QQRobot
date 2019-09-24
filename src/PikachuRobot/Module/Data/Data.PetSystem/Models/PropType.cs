using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.PetSystem.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 11:10:54
    /// @source : 
    /// @des : 道具类型
    /// </summary>
    public class PropType:BaseModel<int>
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Ext { get; set; }

    }
}
