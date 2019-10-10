using Autofac;
using Newbe.Mahua.Plugins.Pikachu.CusJob;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusInterface;
using Newbe.Mahua.Plugins.Pikachu.Domain.Middleware;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/10 14:48:14
    /// @source : 
    /// @des : 
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

        }

    }
}
