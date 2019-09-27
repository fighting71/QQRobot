using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IServiceSupply
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/27 9:54:40
    /// @source : 
    /// @des : 
    /// </summary>
    public class MsgRes<T>
    {

        public bool Success { get; set; }

        public T Data { get; set; }

    }

    public class GroupRes : MsgRes<IEnumerable<GroupItemRes>>
    {
        public static GroupRes Failure = new GroupRes();

        public static GroupRes GetSuccess(params GroupItemRes[] data)
        {
            return new GroupRes { Success = true, Data = data };
        }

        public static GroupRes GetSuccess(IEnumerable<GroupItemRes> data)
        {
            return new GroupRes { Success = true, Data = data };
        }

        public static GroupRes GetSingleSuccess(GroupItemRes info)
        {
            return new GroupRes
            {
                Success = true,
                Data = new [] { info }
            };
        }

        public static implicit operator GroupRes(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            return GetSingleSuccess(str);
        }

    }

    public class PrivateRes : MsgRes<IEnumerable<PrivateItemRes>>
    {

        public static PrivateRes GetSingleSuccess(PrivateItemRes info)
        {
            return new PrivateRes
            {
                Success = true,
                Data = new[] { info }
            };
        }

        public static implicit operator PrivateRes(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            return GetSingleSuccess(str);
        }

    }

    public class PrivateItemRes
    {

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 是否弹一弹(手机端可见)
        /// </summary>
        public bool CallTa { get; set; }


        public static implicit operator PrivateItemRes(string str)
        {
            return new PrivateItemRes() { Msg = str };
        }

    }

    public class GroupItemRes
    {

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 是否@对方
        /// </summary>
        public bool AtTa { get; set; }

        /// <summary>
        /// 是否@所有人
        /// </summary>
        public bool AtAll { get; set; }

        public static implicit operator GroupItemRes(string str)
        {
            return new GroupItemRes() { Msg = str };
        }

    }

}
