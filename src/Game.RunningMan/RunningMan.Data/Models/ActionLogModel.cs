using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunningMan.Data.Menu;

namespace RunningMan.Data.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/17 14:08:27
    /// @source : 
    /// @des : 操作记录
    /// </summary>
    public class ActionLogModel : BaseModel<int>
    {
        public ActionTypes ActionType { get; set; }

        public int ProjectId { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }
    }
}