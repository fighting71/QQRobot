using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu;
using Data.Pikachu.Menu;
using Data.Pikachu.Models;

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
        /// 添加成功次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddSuccessCount(int id)
        {

            var info = PikachuDataContext.ActivityLogs.FirstOrDefault(u => u.Id == id);

            if (info == null) return 0;

            info.SuccessCount++;
            info.UpdateTime = DateTime.Now;

            return PikachuDataContext.SaveChanges();
        }

        /// <summary>
        /// 添加失败次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddFailureCount(int id)
        {

            var info = PikachuDataContext.ActivityLogs.FirstOrDefault(u => u.Id == id);

            if (info == null) return 0;

            info.FailureCount++;
            info.UpdateTime = DateTime.Now;

            return PikachuDataContext.SaveChanges();
        }

        /// <summary>
        /// 关闭活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CloseActivity(int id, string description,out ActivityLog info)
        {
            info = PikachuDataContext.ActivityLogs.FirstOrDefault(u => u.Id == id);

            if (info == null) return 0;

            if (info.ActivityStateType == ActivityStateTypes.Close) return 0;

            info.ActivityStateType = ActivityStateTypes.Close;
            info.EndTime = DateTime.Now;
            info.Description = description;

            return PikachuDataContext.SaveChanges();

        }

        /// <summary>
        /// 获取活动记录
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <returns></returns>
        public ActivityLog GetActivity(int id)
        {
            return PikachuDataContext.ActivityLogs.FirstOrDefault(u=>u.Id == id);
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
