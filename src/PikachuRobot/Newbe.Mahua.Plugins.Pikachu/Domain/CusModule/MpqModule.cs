using Autofac;
using Newbe.Mahua.Apis;
using Newbe.Mahua.MPQ;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using Newbe.Mahua.Plugins.Pikachu.Domain.EventFuns.Mqp;
using Newbe.Mahua.Plugins.Pikachu.MahuaApis.Mpq;
using Newbe.Mahua.Plugins.Pikachu.MahuaEvents.Mpq;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/14 10:41:50
    /// @source : 
    /// @des : mpq平台相关注册
    /// </summary>
    public class MpqModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //// 当前平台是Mpq时才注册这些扩展API
            if (MahuaGlobal.CurrentPlatform == MahuaPlatform.Mpq)
            {
                // 作者名称，既然是你实现了这个功能，那就填上你的名字吧
                var authorName = ConfigConst.Author;

                //builder.RegisterType<EventFunOutput>().As<IEventFunOutput>().InstancePerLifetimeScope();

                builder
                    .RegisterMahuaApi<GetGroupMemebersWithModelApiMahuaCommandHandler,
                        GetGroupMemebersWithModelApiMahuaCommand, GetGroupMemebersWithModelApiMahuaCommandResult>(
                        authorName);

                //builder.RegisterMahuaApi<SendGroupMessageApiMahuaCommandHandler, SendGroupMessageApiMahuaCommand, SendGroupMessageApiMahuaCommandResult>(authorName);

                //builder.RegisterType<RobotSessionContext>().As<IRobotSessionContext>().InstancePerMatchingLifetimeScope((object)MahuaGlobal.LifeTimeScopes.RobotSession);

                builder.RegisterType<CusEventFun10000>().AsImplementedInterfaces().Keyed<IEventFun>(10000);

                builder.RegisterType<SystemInitEvent>()
                    .As<ISystemInitEvent>();
                
            }
        }
    }
}