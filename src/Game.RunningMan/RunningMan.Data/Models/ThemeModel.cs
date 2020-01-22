using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningMan.Data.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/17 11:43:22
    /// @source : 
    /// @des : 主题
    /// </summary>
    public class ThemeModel : BaseModel<int>
    {

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}
