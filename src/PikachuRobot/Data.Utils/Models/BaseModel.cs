using System;
using System.ComponentModel;

namespace Data.Utils.Models
{
    public class BaseModel<T>
    {
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public virtual T Id { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
        
    }
}