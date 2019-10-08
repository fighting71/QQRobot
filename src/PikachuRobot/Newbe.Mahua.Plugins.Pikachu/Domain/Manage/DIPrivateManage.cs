using GenerateMsg.PrivateMsg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Manage
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/8 18:15:39
    /// @source : 
    /// @des : 
    /// </summary>
    public class DIPrivateManage
    {

        public DIPrivateManage(ConfigCacheDeal configCacheDeal, ConfigDeal configDeal)
        {
            ConfigCacheDeal = configCacheDeal;
            ConfigDeal = configDeal;
        }

        public ConfigCacheDeal ConfigCacheDeal { get; }
        public ConfigDeal ConfigDeal { get; }
    }
}
