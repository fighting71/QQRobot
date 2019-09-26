using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateMsg.CusConst
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 16:25:26
    /// @source : 
    /// @des : 
    /// </summary>
    public class CacheConst
    {

        /// <summary>
        /// 添加标识
        /// </summary>
        public const string AddFlag = nameof(AddFlag);

        /// <summary>
        /// 删除标识
        /// </summary>
        public const string RemoveFlag = nameof(RemoveFlag);

        /// <summary>
        /// 成语接龙标识
        /// </summary>
        public const string IdiomsSolitaire = nameof(IdiomsSolitaire);

        /// <summary>
        /// 私聊操作时长
        /// </summary>
        public readonly static TimeSpan PrivateOptExpiry = TimeSpan.FromSeconds(30);

        /// <summary>
        /// 群活动操作时长
        /// </summary>
        public readonly static TimeSpan GroupActivityExpiry = TimeSpan.FromMinutes(30);

        /// <summary>
        /// 获取配置key
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static string GetConfigKey(string account)
        {
            return $"Config_Set_Opt_{account}";
        }

        /// <summary>
        /// 获取群活动key[全体]
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static string GetGroupActivityKey(string group)
        {
            return $"Group_Activity_{group}";
        }

        /// <summary>
        /// 获取成语接龙key[群][缓存词语]
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static string GetIdiomsKey(string group)
        {
            return $"Group_Idioms_{group}";
        }

        /// <summary>
        /// 获取成语接龙日志key[群][缓存尝试次数]
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static string GetIdiomsTryCountKey(string group)
        {
            return $"Group_Idioms_Try_Count_{group}";
        }

    }
}
