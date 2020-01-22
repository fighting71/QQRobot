using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningMan.Data.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/17 11:46:15
    /// @source : 
    /// @des : 
    /// </summary>
    public class BaseModel<T>
    {
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public virtual T Id { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

    }

    public class BaseChangeModel<T> : BaseModel<T>
    {

        public DateTime UpdateTime = DateTime.Now;

    }

}
