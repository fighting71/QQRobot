using IServiceSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.Mahua.MahuaEvents;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 14:44:54
    /// @source : 
    /// @des : 
    /// </summary>
    public class GroupMsgGroup
    {

        private static IList<IGenerateMsg> list = new List<IGenerateMsg>();

        public static void AddDeal(IGenerateMsg deal)
        {
            list.Add(deal);
        }

        public static void AddDeal(int index, IGenerateMsg deal)
        {
            list.Insert(index, deal);
        }

        public static string GetMsg(IMahuaApi mahuaApi, GroupMessageReceivedContext context)
        {
            string res = string.Empty;
            for (int i = 0; i < list.Count && res == string.Empty; i++)
            {
                res = list[i].Run(context, mahuaApi);
            }

            return res;
        }
        
    }
}
