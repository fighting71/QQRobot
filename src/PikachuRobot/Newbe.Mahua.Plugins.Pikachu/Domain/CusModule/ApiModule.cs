using Autofac;
using Newbe.Mahua.Apis;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using Newbe.Mahua.Plugins.Pikachu.MahuaApis.Mpq;
using NLog;
using System;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 10:02:58
    /// @source : 
    /// @des : 
    /// </summary>
    public class ApiModule : Module
    {
        private static readonly Logger _logger = LogManager.GetLogger(nameof(ApiModule));

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //// 当前平台是Mpq时才注册这些扩展API
            if (MahuaGlobal.CurrentPlatform == MahuaPlatform.Mpq)
            {
                // 作者名称，既然是你实现了这个功能，那就填上你的名字吧
                var authorName = ConfigConst.Author;

                builder.RegisterType<EventFunOutput>().As<IEventFunOutput>().InstancePerLifetimeScope();

                builder.RegisterMahuaApi<GetGroupMemebersWithModelApiMahuaCommandHandler, GetGroupMemebersWithModelApiMahuaCommand, GetGroupMemebersWithModelApiMahuaCommandResult>(authorName);

                builder.RegisterType<RobotSessionContext>().As<IRobotSessionContext>().InstancePerMatchingLifetimeScope((object)MahuaGlobal.LifeTimeScopes.RobotSession);

            }


        }
    }

    internal class RobotSessionContext : IRobotSessionContext
    {
        private string _currentQq;

        public string CurrentQq
        {
            get
            {
                return this._currentQq ?? (this._currentQq = this.CurrentQqProvider());
            }
            set
            {
                this._currentQq = value;
            }
        }

        public Func<string> CurrentQqProvider { get; set; }
    }

}
