using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/14 10:42:37
    /// @source : 
    /// @des : 基本模块
    /// </summary>
    public class PluginModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            // 将实现类与接口的关系注入到Autofac的Ioc容器中。如果此处缺少注册将无法启动插件。
            // 注意！！！PluginInfo是插件运行必须注册的，其他内容则不是必要的！！！
            builder.RegisterType<PluginInfo>()
                .As<IPluginInfo>();

            //注册在“设置中心”中注册菜单，若想订阅菜单点击事件，可以查看教程。http://www.newbe.pro/docs/mahua/2017/12/24/Newbe-Mahua-Navigations.html
            builder.RegisterType<MyMenuProvider>()
                .As<IMahuaMenuProvider>();
        }
    }
}
