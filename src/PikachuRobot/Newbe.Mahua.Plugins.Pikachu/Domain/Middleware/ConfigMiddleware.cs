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
        public static string GetConfig(string key)
        {
            // 默认使用appSettings
            return ConfigurationManager.AppSettings[key];
        }

    }
}
