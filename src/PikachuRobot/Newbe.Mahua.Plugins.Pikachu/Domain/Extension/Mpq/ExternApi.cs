using System.Runtime.InteropServices;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Extension.Mpq
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/15 10:23:02
    /// @source : 
    /// @des : api 补充
    /// </summary>
    public class ExternApi
    {
        /// <summary>
        /// 消息撤回 测试成功
        /// 
        /// ps : GroupMessageReceivedContext 没有rawMessage 故使用撤回功能还得自行重新注册个Event2
        /// 
        /// </summary>
        /// <param name="robotQq">响应的QQ</param>
        /// <param name="rawMessage">解析出来的封包内容 </param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern bool Api_CancelGroupMsgA(string robotQq, string rawMessage);

        /// <summary>
        /// Api_SendXml, 逻辑型 太容易被屏蔽了 .... 
        /// </summary>
        /// <param name="account">响应的QQ</param>
        /// <param name="targetType">收信对象类型 1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        /// <param name="group">收信对象所属群_讨论组 发群内、临时会话必填 好友可不填</param>
        /// <param name="receiverAccount">收信对象QQ 临时会话、好友必填 发至群内可不填</param>
        /// <param name="msg"></param>
        /// <param name="structType">结构子类型 00 基本 02 点歌 其他不明</param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern bool Api_SendXml(string account, int targetType, string group, string receiverAccount,
            string msg, int structType);

        /// <summary>
        /// 向对象发送一条音乐信息（所谓的点歌）次数不限
        ///
        /// 测试成功
        ///     参考网易云链接：
        ///     播放地址:https://music.163.com/#/song?id=16607998
        ///     文件地址:http://music.163.com/song/media/outer/url?id=16607998.mp3
        /// 
        /// </summary>
        /// <param name="account">响应的QQ</param>
        /// <param name="targetType">收信对象类型 1好友 2群 3讨论组 4群临时会话 5讨论组临时会话</param>
        /// <param name="groupNo">收信对象所属群_讨论组 发群内、临时会话必填 好友可不填</param>
        /// <param name="receiverAccount">收信对象QQ 临时会话、好友必填 发至群内可不填</param>
        /// <param name="intro">音乐简介 留空默认‘QQ音乐 的分享</param>
        /// <param name="musicLink">音乐播放页面连接 留空为空 无法点开</param>
        /// <param name="faceLink">音乐封面连接 任意直连或短链接均可 可空 例:http://url.cn/cDiJT4</param>
        /// <param name="fileLink">音乐文件直连连接 任意直连或短链接均可 不可空 例:http://url.cn/djwXjr</param>
        /// <param name="musicName">曲名 可空</param>
        /// <param name="author">歌手名 可空</param>
        /// <param name="source">音乐来源名 可空 为空默认QQ音乐</param>
        /// <param name="sourceIconLink">音乐来源图标连接 可空 为空默认QQ音乐</param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern bool Api_SendMusic(string account, int targetType, string groupNo, string receiverAccount,
            string intro, string musicLink, string faceLink, string fileLink, string musicName, string author,
            string source, string sourceIconLink);

        /// <summary>
        /// 发说说 测试无效...
        /// </summary>
        /// <param name="account">响应的QQ, 文本型</param>
        /// <param name="content">内容, 文本型</param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern string Api_AddTaotao(string account, string content);

        /// <summary>
        /// 创建一个讨论组 成功返回讨论组ID 失败返回空 注:每24小时只能创建100个讨论组 悠着点用
        /// 无效
        /// </summary>
        /// <param name="account">响应的QQ</param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern string Api_CreateDG(string account);

        /// <summary>
        /// 邀请对象加入群 失败返回错误理由
        /// </summary>
        /// <param name="account">响应的QQ</param>
        /// <param name="groupNo">群号</param>
        /// <param name="members">成员组, 文本型, , 多个成员用换行符分割</param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern string Api_GroupInvitation(string account,string members,string groupNo);

        /// <summary>
        /// 邀请对象加入讨论组 成功返回空 失败返回理由
        /// </summary>
        /// <param name="account">响应的QQ</param>
        /// <param name="groupId">讨论组ID</param>
        /// <param name="members">成员组, 文本型, , 多个成员用换行符分割</param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern string Api_DGInvitation(string account,string groupId,string members);

        /// <summary>
        /// 成功返回以换行符分割的讨论组号列表.最大为100个讨论组  失败返回空
        /// 
        /// </summary>
        /// <param name="account">响应的QQ</param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern string Api_GetDGList(string account);

        /// <summary>
        /// 文本型, 通过qun.qq.com接口取得群成员列表 成功返回转码后的JSON格式文本
        /// </summary>
        /// <param name="account">响应的QQ</param>
        /// <param name="groupNo">群号</param>
        /// <returns></returns>
        [DllImport("message.dll")]
        public static extern string Api_GetGroupMemberA(string account,string groupNo);

    }
}