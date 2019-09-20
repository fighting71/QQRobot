using Newbe.Mahua.Plugins.Pikachu.Domain.Middleware;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusConst
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 10:55:52
    /// @source : 
    /// @des : 
    /// </summary>
    public class ConfigConst
    {

        public static string Version = ConfigMiddleware.GetConfig(nameof(Version));
        public static string AppName = ConfigMiddleware.GetConfig(nameof(AppName));
        public static string Author = ConfigMiddleware.GetConfig(nameof(Author));
        public static string AppId = ConfigMiddleware.GetConfig(nameof(AppId));
        public static string Description = ConfigMiddleware.GetConfig(nameof(Description));
        public static string DefaultGroupMsg = ConfigMiddleware.GetConfig(nameof(DefaultGroupMsg));
        public static string DefaultPrivateMsg = ConfigMiddleware.GetConfig(nameof(DefaultPrivateMsg));

    }
}
