using Autofac;
using Newbe.Mahua.Plugins.Pikachu.Domain.Middleware;
using PikachuRobot.Job.Hangfire;
using PikachuRobot.Job.Hangfire.Host;
using PikachuRobot.Job.Hangfire.Job;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/10 14:48:14
    /// @source : 
    /// @des : job 相关注册
    /// </summary>
    public class JobModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<OwinWebHost>()
                .As<IWebHost>()
                .SingleInstance();

            builder.RegisterType<TestJob>().AsSelf();
            
            builder.RegisterType<AutoCloseGroupActivityJob>().AsSelf();
            builder.RegisterType<AutoOutGroupMsg>().AsSelf();
        }
    }
}