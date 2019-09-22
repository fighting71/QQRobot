using Commond.Tools;
using Data.Pikachu;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Middleware
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 11:21:15
    /// @source : 
    /// @des : 
    /// </summary>
    public class ConfigMiddleware
    {

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key,string defaultValue = null)
        {

            return ConfigurationManager.AppSettings[key];

            // bug
            // 构建时 读取不到dbContext....
            //PikachuDataContext context = InstanceFactory.Get<PikachuDataContext>();

            //// 从db中获取
            //return context.configInfos.FirstOrDefault(u => u.Enable && key.Equals(key, StringComparison.CurrentCultureIgnoreCase))?.Value ?? defaultValue;

            // 默认使用appSettings
            //return ConfigurationManager.AppSettings[key];
        }

    }
}
