using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu.Menu;

namespace Data.Pikachu.Models
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/15 16:01:00
    /// @source : 
    /// @des : 提醒任务
    /// </summary>
    public class JobConfig : BaseModel<int>
    {
        /// <summary>
        /// 机器人账号
        /// </summary>
        public string RobotAccount { get; set; }

        /// <summary>
        /// 提醒类型
        /// </summary>
        public ConfirmTypes ConfirmType { get; set; }
        
        /// <summary>
        /// Cron表达式.
        /// </summary>
        public string CronExpression { get; set; }
        
        /// <summary>
        /// 群组
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        /// 目标人物
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 标题 公告专用
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// 提示模板
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// 参数数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Enable { get; set; }
    }
}