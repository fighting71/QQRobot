using Autofac;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Owin;
using NLog;
using Owin;

// 这是Startup的入口标记
[assembly: OwinStartup(typeof(Newbe.Mahua.Plugins.Pikachu.StartUp))]
namespace Newbe.Mahua.Plugins.Pikachu
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/10 15:00:16
    /// @source : 
    /// @des : 
    /// </summary>
    public class StartUp
    {

        private static readonly Logger Logger = LogManager.GetLogger(nameof(StartUp));

        public void Configuration(IAppBuilder app, ILifetimeScope lifetimeScope)
        {
            // 初始化Hangfire
            var config = GlobalConfiguration.Configuration;

            // 使用内存存储任务，若有持久化任务的需求，可以根据Hangfire的文档使用数据库方式存储
            config.UseMemoryStorage();

            // 通过Autofac容器来实现任务的构建
            config.UseAutofacActivator(lifetimeScope);

            // 启用Hangfire的web界面
            app.UseHangfireDashboard();

            // 初始化Hangfire服务
            app.UseHangfireServer();
        }


    }
}
