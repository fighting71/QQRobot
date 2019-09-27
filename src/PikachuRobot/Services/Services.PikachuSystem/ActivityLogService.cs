using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu;
using Data.Pikachu.Menu;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/27 11:11:00
    /// @source : 
    /// @des : 
    /// </summary>
    public class ActivityLogService : BaseService
    {
        public ActivityLogService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        /// <summary>
        /// 开启活动并返回自增id
        /// </summary>
        /// <param name="group">群组</param>
        /// <param name="type">活动类型</param>
        /// <param name="endTime">结束时间(预计)</param>
        /// <returns></returns>
        public int OpenActivity(string group, ActivityTypes type, DateTime endTime)
        {
            var info = new Data.Pikachu.Models.ActivityLog()
            {
                ActivityStateType = ActivityStateTypes.Open,
                Group = group,
                ActivityType = type,
                PredictEndTime = endTime,
            };

            PikachuDataContext.ActivityLogs.Add(info);

            PikachuDataContext.SaveChanges();

            return info.Id;

        }

    }
}
