using System;

namespace Domain.Command.CusConst
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/27 10:40:52
    /// @source : 
    /// @des : 规则常量
    /// </summary>
    public class RuleConst
    {

        /// <summary>
        /// 成语接龙尝试次数
        /// </summary>
        public const long IdiomsMaxTryCount = 6;

        /// <summary>
        /// 私聊操作时长
        /// </summary>
        public static readonly TimeSpan PrivateOptExpiry = TimeSpan.FromSeconds(30);

        /// <summary>
        /// 群活动操作时长
        /// </summary>
        public static readonly TimeSpan GroupActivityExpiry = TimeSpan.FromMinutes(5);

    }
}
