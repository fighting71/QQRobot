using System;
using System.ComponentModel;

namespace Data.Pikachu.Models
{
    public class BaseModel<T>
    {
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]//不自动增长
        public virtual T Id { get; set; }
        
        public DateTime? CreateTime { get; set; }
        
        public DateTime? UpdateTime { get; set; }
        
    }
}