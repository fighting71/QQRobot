using Newbe.Mahua.Plugins.Pikachu.Domain.Middleware;

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

        public static readonly string Version = ConfigMiddleware.GetConfig(nameof(Version));
        public static readonly string AppName = ConfigMiddleware.GetConfig(nameof(AppName));
        public static readonly string Author = ConfigMiddleware.GetConfig(nameof(Author));
        public static readonly string AppId = ConfigMiddleware.GetConfig(nameof(AppId));
        public static readonly string Description = ConfigMiddleware.GetConfig(nameof(Description));
        public static readonly string DefaultGroupMsg = ConfigMiddleware.GetConfig(nameof(DefaultGroupMsg));
        public static readonly string DefaultPrivateMsg = ConfigMiddleware.GetConfig(nameof(DefaultPrivateMsg));
        public static readonly string RedisClient = ConfigMiddleware.GetConfig(nameof(RedisClient));
        public static readonly string RedisDb = ConfigMiddleware.GetConfig(nameof(RedisDb));
        public static readonly string HangFireBaseUrl = ConfigMiddleware.GetConfig(nameof(HangFireBaseUrl));

    }
}
