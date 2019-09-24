using Data.PetSystem.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.PetSystem.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 10:00:24
    /// @source : 
    /// @des : 宠物信息
    /// </summary>
    public class PetInfo:BaseModel<int>
    {

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 宠物名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 初始级别
        /// </summary>
        public int InitLevel { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public long Price { get; set; }

        /// <summary>
        /// 宠物头像[介绍图]
        /// </summary>
        public string Face { get; set; }

        /// <summary>
        /// 武力值
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        public int Intellect { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}
