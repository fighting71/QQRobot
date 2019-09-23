using IServiceSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;

namespace GenerateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 14:39:26
    /// @source : 
    /// @des : 
    /// </summary>
    public class RegexBaseDeal : IGroupMsgDeal
    {
        public RegexBaseDeal(string regex, string result) : this(regex, () => result)
        {
        }

        public RegexBaseDeal(string regex, Func<string> resultFunc)
        {
            this._regex = regex;
            _getResultFunc = resultFunc;
        }

        private readonly string _regex;
        private readonly Func<string> _getResultFunc;

        public string Run(GroupMessageReceivedContext context, IMahuaApi mahuaApi)
        {
            if (!Regex.IsMatch(context.Message, _regex, RegexOptions.Multiline))
            {
                return string.Empty;
            }

            return _getResultFunc();
        }
    }
}