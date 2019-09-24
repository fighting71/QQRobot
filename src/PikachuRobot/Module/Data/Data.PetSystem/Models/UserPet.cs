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
    /// @since : 2019/9/24 10:47:01
    /// @source : 
    /// @des : 用户宠物信息
    /// </summary>
    public class UserPet:BaseModel<int>
    {


        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enable { get; set; }

        public int PetId { get; set; }

        public int Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Sex { get; set; }

        /// <summary>
        /// 宠物头像[介绍图]
        /// </summary>
        public string Face { get; set; }

        /// <summary>
        /// 宠物主人
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 健康值
        /// </summary>
        public int Healthy { get; set; }

        /// <summary>
        /// 心情
        /// </summary>
        public int Mood { get; set; }

        /// <summary>
        /// 武力值
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        public int Intellect { get; set; }

        /// <summary>
        /// 品质
        /// </summary>
        public int Quality { get; set; }

    }
}
