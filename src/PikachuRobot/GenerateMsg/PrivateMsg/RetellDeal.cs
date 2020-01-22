using IServiceSupply;
using Services.PikachuSystem;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenerateMsg.PrivateMsg
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 16:40:05
    /// @source : 
    /// @des : 复述
    /// </summary>
    public class RetellDeal : IGeneratePrivateMsgDeal
    {

        public async Task<PrivateRes> Run(string msg, string account, Lazy<string> getLoginAccount)
        {
            Match match;

            if ((match = Regex.Match(msg, @"^复述([\s|\S]*)$")).Success)
            {
                return match.Groups[1].Value;
            }

            return null;
        }
    }
}