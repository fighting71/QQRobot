using Data.Pikachu;
using Data.Pikachu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 11:30:21
    /// @source : 
    /// @des : 
    /// </summary>
    public class ConfigService:BaseService
    {
        public ConfigService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }


        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        public IQueryable<ConfigInfo> GetAll()
        {
            return PikachuDataContext.ConfigInfos.Where(u => u.Enable);
        }

        /// <summary>
        /// 移除配置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="msg"></param>
        public void RemoveKey(string key,out string msg)
        {
            var search =
                PikachuDataContext.ConfigInfos.FirstOrDefault(u => u.Enable && u.Key.Equals(key));
            if (search != null)
            {
                search.Enable = false;
                PikachuDataContext.SaveChanges();
            }

            msg = "删除成功";
        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="input"></param>
        /// <param name="msg"></param>
        public void AddInfo(string input,out string msg)
        {
            var info = input.Split('|');
            if (info.Length == 3)
            {
                if (!string.IsNullOrWhiteSpace(info[0]))
                {
                    var config = new ConfigInfo()
                    {
                        Key = info[0].Trim(),
                        Value = info[1],
                        Description = info[2],
                        Enable = true
                    };

                    var old = PikachuDataContext.ConfigInfos.FirstOrDefault(u =>
                        u.Enable && u.Key.Equals(config.Key, StringComparison.CurrentCultureIgnoreCase));

                    if (old != null)
                    {
                        old.Value = config.Value;
                        old.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        config.UpdateTime = DateTime.Now;
                        PikachuDataContext.ConfigInfos.Add(config);
                    }

                    PikachuDataContext.SaveChanges();

                    msg = "   添加成功！";
                }
                else
                {
                    msg = "   配置key不能为空！";
                }
            }
            else
            {
                msg = "   输入格式有误！";
            }
        }

        
        /// <summary>
        /// 移除配置
        /// </summary>
        /// <param name="key"></param>
        public async Task RemoveKeyAsync(string key)
        {
            var search =
                PikachuDataContext.ConfigInfos.FirstOrDefault(u => u.Enable && u.Key.Equals(key));
            if (search != null)
            {
                search.Enable = false;
                await PikachuDataContext.SaveChangesAsync();
            }

        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="key">配置key</param>
        /// <param name="value">配置value</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public async Task AddInfoAsync(string key,string value,string description)
        {
            var config = new ConfigInfo()
            {
                Key = key,
                Value = value,
                Description = description,
                Enable = true
            };

            var old = await PikachuDataContext.ConfigInfos.FirstOrDefaultAsync(u =>
                u.Enable && u.Key.Equals(config.Key, StringComparison.CurrentCultureIgnoreCase));

            if (old != null)
            {
                old.Value = config.Value;
                old.UpdateTime = DateTime.Now;
            }
            else
            {
                config.UpdateTime = DateTime.Now;
                PikachuDataContext.ConfigInfos.Add(config);
            }

            await PikachuDataContext.SaveChangesAsync();
        }
        
    }
}
