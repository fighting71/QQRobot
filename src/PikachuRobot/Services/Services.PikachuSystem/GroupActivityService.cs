using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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
    public class GroupActivityService : BaseService
    {
        public GroupActivityService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        /// <summary>
        /// 添加成功次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddSuccessCount(int id)
        {
            var info = PikachuDataContext.GroupActivities.FirstOrDefault(u => u.Id == id);

            if (info == null) return 0;

            info.SuccessCount++;
            info.UpdateTime = DateTime.Now;

            return PikachuDataContext.SaveChanges();
        }

        /// <summary>
        /// 添加成功次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> AddSuccessCountAsync(int id)
        {
            var info = await PikachuDataContext.GroupActivities.FirstOrDefaultAsync(u => u.Id == id);

            if (info == null) return 0;

            info.SuccessCount++;
            info.UpdateTime = DateTime.Now;

            return await PikachuDataContext.SaveChangesAsync();
        }

        /// <summary>
        /// 添加失败次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AddFailureCount(int id)
        {
            var info = PikachuDataContext.GroupActivities.FirstOrDefault(u => u.Id == id);

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
        public int CloseActivity(int id, string description, out GroupActivity info)
        {
            info = PikachuDataContext.GroupActivities.FirstOrDefault(u => u.Id == id);

            if (info == null || info.ActivityStateType == ActivityStateTypes.Close) return 0;

            if (info.ActivityStateType == ActivityStateTypes.Close) return 0;

            info.ActivityStateType = ActivityStateTypes.Close;
            info.EndTime = DateTime.Now;
            info.Description = description;

            return PikachuDataContext.SaveChanges();
        }

        /// <summary>
        /// 关闭活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GroupActivity> CloseActivityAsync(int id, string description)
        {
            var info = await PikachuDataContext.GroupActivities.FirstOrDefaultAsync(u => u.Id == id);

            if (info == null || info.ActivityStateType == ActivityStateTypes.Close) return info;

            if (info.ActivityStateType == ActivityStateTypes.Close) return info;

            info.ActivityStateType = ActivityStateTypes.Close;
            info.EndTime = DateTime.Now;
            info.Description = description;

            await PikachuDataContext.SaveChangesAsync();

            return info;
        }

        /// <summary>
        /// 获取活动记录
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <returns></returns>
        public GroupActivity GetActivity(int id)
        {
            return PikachuDataContext.GroupActivities.FirstOrDefault(u => u.Id == id);
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
            var info = new Data.Pikachu.Models.GroupActivity()
            {
                ActivityStateType = ActivityStateTypes.Open,
                Group = group,
                ActivityType = type,
                PredictEndTime = endTime,
            };

            PikachuDataContext.GroupActivities.Add(info);

            PikachuDataContext.SaveChanges();

            return info.Id;
        }

        /// <summary>
        /// 开启活动并返回自增id
        /// </summary>
        /// <param name="group">群组</param>
        /// <param name="type">活动类型</param>
        /// <param name="endTime">结束时间(预计)</param>
        /// <returns></returns>
        public async Task<int> OpenActivityAsync(string group, ActivityTypes type, DateTime endTime)
        {
            var info = new GroupActivity()
            {
                ActivityStateType = ActivityStateTypes.Open,
                Group = group,
                ActivityType = type,
                PredictEndTime = endTime,
            };

            PikachuDataContext.GroupActivities.Add(info);

            await PikachuDataContext.SaveChangesAsync();

            return info.Id;
        }

        /// <summary>
        /// 添加失败次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> AddFailureCountAsync(int id)
        {
            var info = await PikachuDataContext.GroupActivities.FirstOrDefaultAsync(u => u.Id == id);

            if (info == null) return 0;

            info.FailureCount++;
            info.UpdateTime = DateTime.Now;

            return await PikachuDataContext.SaveChangesAsync();
        }

        /// <summary>
        /// 自动关闭活动
        /// </summary>
        /// <returns></returns>
        public async Task<List<GroupActivity>> AutoCloseAsync()
        {
            // 直接sql操作
//            PikachuDataContext.Database.Connection.Execute(@"
//SELECT ActivityType
//	FROM activitylogs
//WHERE EndTime IS NULL AND PredictEndTime <= NOW()
//");

            var list = await PikachuDataContext.GroupActivities
                .Where(u => u.ActivityStateType == ActivityStateTypes.Open && u.PredictEndTime < DateTime.Now)
                .ToListAsync();

            foreach (var item in list)
            {
                item.EndTime = DateTime.Now;
                item.ActivityStateType = ActivityStateTypes.Close;
                item.Description = "job 自动关闭";
            }

            await PikachuDataContext.SaveChangesAsync();

            return list;
        }
    }
}