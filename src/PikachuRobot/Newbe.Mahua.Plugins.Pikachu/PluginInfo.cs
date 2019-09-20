using Newbe.Mahua;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;

namespace Newbe.Mahua.Plugins.Pikachu
{
    /// <summary>
    /// 本插件的基本信息
    /// </summary>
    public class PluginInfo : IPluginInfo
    {
        /// <summary>
        /// 版本号，建议采用 主版本.次版本.修订号 的形式
        /// </summary>
        public string Version { get; set; } = ConfigConst.AppName ?? "1.0.0";// 避免构建时异常

        /// <summary>
        /// 插件名称
        /// </summary>

        public string Name { get; set; } = ConfigConst.AppName;

        /// <summary>
        /// 作者名称
        /// </summary>
        public string Author { get; set; } = ConfigConst.Author;

        /// <summary>
        /// 插件Id，用于唯一标识插件产品的Id，至少包含 AAA.BBB.CCC 三个部分
        /// </summary>
        public string Id { get; set; } = ConfigConst.AppId;

        /// <summary>
        /// 插件描述
        /// </summary>
        public string Description { get; set; } = ConfigConst.Description;
    }
}
