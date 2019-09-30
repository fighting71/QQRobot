using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceSupply
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/30 13:42:27
    /// @source : 
    /// @des : 处理私聊回复
    /// </summary>
    public interface IGeneratePrivateMsgDeal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">收到消息</param>
        /// <param name="account">发送人</param>
        /// <param name="getLoginAccount">接收人</param>
        /// <returns></returns>
        Task<PrivateRes> Run(string msg, string account, Lazy<string> getLoginAccount);
    }
    public delegate Task<PrivateRes> GeneratePrivateMsgDel(string msg, string account, Lazy<string> getLoginAccount);
}