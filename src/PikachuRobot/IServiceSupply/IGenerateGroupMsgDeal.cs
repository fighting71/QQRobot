using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceSupply
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/30 13:51:58
    /// @source : 
    /// @des : 处理群聊信息
    /// </summary>
    public interface IGenerateGroupMsgDeal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="account">发送人</param>
        /// <param name="groupNo">群号</param>
        /// <param name="getLoginAccount">接收人</param>
        /// <returns></returns>
        Task<GroupRes> Run(string msg, string account,string groupNo, Lazy<string> getLoginAccount);
    }
    
    public delegate Task<GroupRes> GenerateGroupMsgDel(string msg, string account,string groupNo, Lazy<string> getLoginAccount);
}