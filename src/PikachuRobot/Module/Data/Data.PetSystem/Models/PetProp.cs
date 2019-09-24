using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.PetSystem.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 10:42:56
    /// @source : 
    /// @des : 宠物道具
    /// </summary>
    public class PetProp:BaseModel<int>
    {

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 道具名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 道具类型
        /// </summary>
        public int PropType { get; set; }

        /// <summary>
        /// 道具价格
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


    }
}
