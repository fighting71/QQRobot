using System.Data.Entity;
using Autofac;
using NLog;
using System.Linq;
using System.Threading;
using Newbe.Mahua.Plugins.Pikachu.Domain.Manage;
using Data.Pikachu;
using GenerateMsg.PrivateMsg;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using StackExchange.Redis;
using IServiceSupply;
using GenerateMsg.GroupMsg;
using Data.PetSystem;
using Data.Pikachu.Menu;
using Services.PikachuSystem;
using Services.PetSystem;
using Services.Utils;
using Data.Utils;
using Newbe.Mahua.Plugins.Pikachu.MahuaEvents;
using Newbe.Mahua.MahuaEvents;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 15:09:34
    /// @source : 
    /// @des : 自定义配置module
    /// </summary>
    public class InitModule : Module
    {
        private static readonly Logger Logger = LogManager.GetLogger(nameof(MahuaModule));

        private static ConnectionMultiplexer _ctx;

        private IDatabase GetDatabase()
        {
            int.TryParse(ConfigConst.RedisDb, out var db);
            return _ctx.GetDatabase(db);
        }

        public InitModule()
        {

        }


        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            try
            {
                _ctx = ConnectionMultiplexer.Connect(ConfigConst.RedisClient);

                builder.Register(context => GetDatabase());

                // 注册db
                builder.RegisterType<PikachuDataContext>().InstancePerLifetimeScope();
                builder.RegisterType<PetContext>().InstancePerLifetimeScope();
                builder.RegisterType<UtilsContext>().InstancePerLifetimeScope();

                // 注册service
                RegisterUtilService(builder);
                
                RegisterPetService(builder);
                
                RegisterPikachuService(builder);

                // 注册deal
                RegisterGroupMsgDeal(builder);
                
                RegisterPrivateMsgDeal(builder);
                
                builder.RegisterType<GroupMsgManage>().As<IGenerateGroupMsgDeal>();

                builder.RegisterType<PrivateMsgManage>().As<IGeneratePrivateMsgDeal>();

            }
            catch (System.Exception e)
            {
                Logger.Error(e);
            }
        }


        private void RegisterGroupMsgDeal(ContainerBuilder builder)
        {
            // builder.RegisterAssemblyTypes(typeof(SignDeal).Assembly);
            builder.RegisterType<AddPetCacheDeal>().InstancePerLifetimeScope();
            builder.RegisterType<GroupConfigDeal>().InstancePerLifetimeScope();
            builder.RegisterType<IdiomsSolitaireCacheDeal>().InstancePerLifetimeScope();
            builder.RegisterType<IdiomsSolitaireDeal>().InstancePerLifetimeScope();
            builder.RegisterType<MemberAmountDeal>().InstancePerLifetimeScope();
            builder.RegisterType<PetDeal>().InstancePerLifetimeScope();
            builder.RegisterType<SignDeal>().InstancePerLifetimeScope();
        }

        private void RegisterPrivateMsgDeal(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigCacheDeal>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigDeal>().InstancePerLifetimeScope();
            builder.RegisterType<GroupAuthDeal>().InstancePerLifetimeScope();
            builder.RegisterType<GroupMsgCopyDeal>().InstancePerLifetimeScope();
        }

        private void RegisterUtilService(ContainerBuilder builder)
        {
            builder.RegisterType<IdiomsService>().InstancePerLifetimeScope();
        }

        private void RegisterPikachuService(ContainerBuilder builder)
        {
            builder.RegisterType<BillFlowService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigService>().InstancePerLifetimeScope();
            builder.RegisterType<GroupConfigService>().InstancePerLifetimeScope();
            builder.RegisterType<GroupAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<GroupMsgCopyService>().InstancePerLifetimeScope();
            builder.RegisterType<ManageService>().InstancePerLifetimeScope();
            builder.RegisterType<MemberInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<GroupActivityService>().InstancePerLifetimeScope();
        }
        
        private void RegisterPetService(ContainerBuilder builder)
        {
            builder.RegisterType<PetService>().InstancePerLifetimeScope();
            builder.RegisterType<UserPetService>().InstancePerLifetimeScope();
        }
        
    }
}