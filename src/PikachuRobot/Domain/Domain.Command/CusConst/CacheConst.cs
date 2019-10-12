namespace Domain.Command.CusConst
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 16:25:26
    /// @source : 
    /// @des : 缓存常量
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
        /// 添加宠物标识
        /// </summary>
        public const string AddPet = nameof(AddPet);

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
        /// 获取成语接龙key[群][缓存尝试次数]
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static string GetIdiomsTryCountKey(string group)
        {
            return $"Group_Idioms_Try_Count_{group}";
        }

        /// <summary>
        /// 获取成语接龙key[群][日志记录]
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static string GetActivityLogKey(string group)
        {
            return $"Group_Idioms_Log_Id_{group}";
        }

        /// <summary>
        /// 群聊消息缓存key
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static string GetGroupMsgListKey(string group)
        {
            return $"Group_Msg_List_{group}";
        }

        /// <summary>
        /// 获取成员操作key
        /// </summary>
        /// <param name="account"></param>
        /// <param name="group"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string GetMemberOptKey(string account, string group, string flag)
        {
            return $"Member_Opt_{account}_{group}_{flag}";
        }
    }
}