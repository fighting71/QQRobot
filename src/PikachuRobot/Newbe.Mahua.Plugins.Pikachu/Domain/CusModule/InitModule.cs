using Autofac;
using NLog;
using System.Linq;
using System.Threading;
using Commond.Tools;
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
using Newbe.Mahua.Plugins.Pikachu.Domain.Factory;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusModule
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/20 15:09:34
    /// @source : 
    /// @des : 
    /// </summary>
    public class InitModule : Module
    {
        private static readonly Logger Logger = LogManager.GetLogger(nameof(MahuaModule));

        private static readonly ConnectionMultiplexer ctx = ConnectionMultiplexer.Connect(ConfigConst.RedisClient);

        private T Get<T>() where T : class, new()
        {
            return ThreadInstanceFactory<T>.Get((() => new T()));
        }

        public InitModule()
        {
            Logger.Debug("开始进行初始化");
        }


        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PikachuDataContext>(); // 此处若是注入单例 会引起 context 随着依赖的对象释放而释放

            builder.Register(context => InitGroupMsgManage()).As<IGroupMsgDeal>().SingleInstance();
            builder.Register(context => InitPrivateMsgManage()).As<IPrivateMsgDeal>().SingleInstance();
        }

        private IPrivateMsgDeal InitPrivateMsgManage()
        {
            int.TryParse(ConfigConst.RedisDb, out var db);

            PrivateMsgManage manage = new PrivateMsgManage();

            manage
                .AddDeal(
                    () => new ConfigCacheDeal(ctx.GetDatabase(db), new ConfigService(
                        Get<PikachuDataContext>()
                    )).Run)
                .AddDeal(() => new ConfigDeal(ctx.GetDatabase(db), new ConfigService(
                    Get<PikachuDataContext>()
                )).Run)
                .AddDeal(
                    () => new GroupManageDeal(new GroupManageService(
                        Get<PikachuDataContext>()
                    )).Run)
                .AddDeal(
                    () => new GroupMsgCopyDeal(new GroupMsgCopyService(
                        Get<PikachuDataContext>()
                    )).Run)
                .AddDeal(() => (context, api) =>
                {
                    return Get<PikachuDataContext>().ConfigInfos
                        .FirstOrDefault(u => u.Enable && u.Key.Equals("Private.Confirm.Default"))?.Value;
                });

            return manage;
        }


        private IGroupMsgDeal InitGroupMsgManage()
        {
            GroupMsgManage manage = new GroupMsgManage();

            manage
                .AddDeal(() => new PetDeal(new PetService(new PetContext())).Run)
                .AddDeal(() => new GroupConfigDeal(
                    new ManageService(Get<PikachuDataContext>()),
                    new GroupConfigService(Get<PikachuDataContext>())
                ).Run)
                .AddDeal(() => (context, api) =>
                    {
                        var pikachuDataContext = Get<PikachuDataContext>();

                        var account = api.GetLoginQq();

                        return pikachuDataContext.GroupConfigs.FirstOrDefault(u =>
                            u.Enable && u.GetGroupConfigType == GroupConfigTypes.DefaultConfirm
                                     && u.Account.Equals(account) && u.Group.Equals(context.FromGroup))?.Value;
                    }
                );

            return manage;
        }
    }
}