using Data.Pikachu.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Pikachu.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/27 10:57:24
    /// @source : 
    /// @des : 群组活动
    /// </summary>
    public class GroupActivity:BaseModel<int>
    {

        /// <summary>
        /// 活动类型
        /// </summary>
        public ActivityTypes ActivityType { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public ActivityStateTypes ActivityStateType { get; set; }

        /// <summary>
        /// 预计结束时间
        /// </summary>
        public DateTime PredictEndTime { get; set; }

        /// <summary>
        /// 结束时间(实际)
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 成功次数
        /// </summary>
        public int SuccessCount { get; set; }

        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailureCount { get; set; }

        /// <summary>
        /// 群组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

    }
}
