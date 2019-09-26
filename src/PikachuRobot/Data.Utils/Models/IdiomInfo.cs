using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 15:58:24
    /// @source : 
    /// @des : 成语
    /// </summary>
    public class IdiomInfo : BaseModel<int>
    {

        /// <summary>
        /// 来源
        /// </summary>
        public string Derivation { get; set; }

        /// <summary>
        /// 示例
        /// </summary>
        public string Example { get; set; }

        /// <summary>
        /// 解释
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string Spell { get; set; }

        /// <summary>
        /// 首拼
        /// </summary>
        public string FirstSpell { get; set; }

        /// <summary>
        /// 尾拼
        /// </summary>
        public string LastSpell { get; set; }

        /// <summary>
        /// 成语
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// 首字母拼音
        /// </summary>
        public string Abbreviation { get; set; }

    }
}
