using Autofac;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenerateMsg;
using Newbe.Mahua.Plugins.Pikachu.Domain.Manage;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 15:09:34
    /// @source : 
    /// @des : 
    /// </summary>
    public class InitModule: Module
    {

        private static readonly Logger _logger = LogManager.GetLogger(nameof(MahuaModule));

        public InitModule()
        {
            _logger.Debug("开始进行初始化");
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            
            GroupMsgGroup.AddDeal(new RegexBaseDeal(@"[\s|\n|\r]*菜单列表[\s|\n|\r]*",@"
当前支持内容:
    [公告设置] [新人提示] [退群提示]  
"));
            
            GroupMsgGroup.AddDeal(new NoticeDeal());
            
        }
    }
}
