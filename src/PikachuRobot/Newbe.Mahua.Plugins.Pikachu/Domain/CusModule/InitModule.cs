using System.Data.Entity;
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
using Services.Utils;
using Data.Utils;

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

        private static T Get<T>() where T : class, new()
        {
            return ThreadInstanceFactory<T>.Get((() => new T()));
        }

        private IDatabase GetDatabase()
        {
            int.TryParse(ConfigConst.RedisDb, out var db);
            return _ctx.GetDatabase(db);
        }

        public InitModule()
        {
            Logger.Debug("开始进行初始化");
        }


        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            try
            {
                _ctx = ConnectionMultiplexer.Connect(ConfigConst.RedisClient);

                builder.Register(context => GetDatabase());
                builder.RegisterType<PikachuDataContext>(); // 此处若是注入单例 会引起 context 随着依赖的对象释放而释放

                builder.Register(context => InitGroupMsgManage()).SingleInstance();
                builder.Register(context => InitPrivateMsgManage()).SingleInstance();

                //builder.Register(context => Get<PikachuDataContext>());
                builder.Register(context => Get<PetContext>());
                builder.Register(context => Get<UtilsContext>());
                builder.RegisterType<PetService>();
                builder.RegisterType<UserPetService>();
                
                builder.RegisterType<BillFlowService>();
                builder.RegisterType<ConfigService>();
                builder.RegisterType<GroupConfigService>();
                builder.RegisterType<GroupManageService>();
                builder.RegisterType<GroupMsgCopyService>();
                builder.RegisterType<ManageService>();
                builder.RegisterType<MemberInfoService>();
                
                builder.RegisterType<IdiomsService>();
                
                builder.RegisterType<ConfigCacheDeal>();
                builder.RegisterType<ConfigDeal>();
                builder.RegisterType<GroupManageDeal>();
                builder.RegisterType<GroupMsgCopyDeal>();

                builder.RegisterType<DIPrivateManage>();

            }
            catch (System.Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// 初始化私聊消息处理管道
        /// </summary>
        /// <returns></returns>
        private IGeneratePrivateMsgDeal InitPrivateMsgManage()
        {
            PrivateMsgManage manage = new PrivateMsgManage();

            manage
                .AddDeal(
                    () => new ConfigCacheDeal(GetDatabase(), new ConfigService(
                        Get<PikachuDataContext>()
                    )).Run, nameof(ConfigCacheDeal))
                .AddDeal(() => new ConfigDeal(GetDatabase(), new ConfigService(
                    Get<PikachuDataContext>()
                )).Run, nameof(ConfigDeal))
                .AddDeal(
                    () => new GroupManageDeal(new GroupManageService(
                        Get<PikachuDataContext>()
                    )).Run)
                .AddDeal(
                    () => new GroupMsgCopyDeal(new GroupMsgCopyService(
                        Get<PikachuDataContext>()
                    )).Run)
                .AddDeal(() => async (context, api, getLoginQq) =>
                {
                    var info = await Get<PikachuDataContext>().ConfigInfos
                        .FirstOrDefaultAsync(u => u.Enable && u.Key.Equals("Private.Confirm.Default"));

                    return info?.Value;
                });

            return manage;
        }

        /// <summary>
        /// 初始化群聊消息处理管道
        /// </summary>
        /// <returns></returns>
        private IGenerateGroupMsgDeal InitGroupMsgManage()
        {
            GroupMsgManage manage = new GroupMsgManage();

            manage
                .AddDeal((() => new AddPetCacheDeal(
                    GetDatabase(),new UserPetService(Get<PetContext>()),
                    new MemberInfoService(Get<PikachuDataContext>()),new PetService(Get<PetContext>()) ,
                    new BillFlowService(Get<PikachuDataContext>())
                    ).Run))
                .AddDeal(() => new IdiomsSolitaireCacheDeal(
                    new IdiomsService(Get<UtilsContext>()),
                    GetDatabase(),
                    new BillFlowService(Get<PikachuDataContext>()),
                    new MemberInfoService(Get<PikachuDataContext>()),
                    new ActivityLogService(Get<PikachuDataContext>()),
                    new ManageService(Get<PikachuDataContext>())
                ).Run)
                .AddDeal(() => new IdiomsSolitaireDeal(
                    new IdiomsService(Get<UtilsContext>()),
                    GetDatabase(),
                    new ActivityLogService(Get<PikachuDataContext>())
                ).Run)
                .AddDeal(() => new MemberAmountDeal(new MemberInfoService(Get<PikachuDataContext>())).Run)
                .AddDeal(() => new SignDeal(new BillFlowService(Get<PikachuDataContext>()),
                    new MemberInfoService(Get<PikachuDataContext>())).Run)
                .AddDeal(() =>
                    new PetDeal(
                        new PetService(new PetContext()), new MemberInfoService(Get<PikachuDataContext>()),
                        new UserPetService(Get<PetContext>()), GetDatabase()
                    ).Run)
                .AddDeal(() => new GroupConfigDeal(
                    new ManageService(Get<PikachuDataContext>()),
                    new GroupConfigService(Get<PikachuDataContext>())
                ).Run)
                .AddDeal(() => async (msg, account, groupNo, getLoginAccount) =>
                    {
                        var loginQq = getLoginAccount.Value;

                        var info = await Get<PikachuDataContext>().GroupConfigs.FirstOrDefaultAsync(u =>
                            u.Enable && u.GetGroupConfigType == GroupConfigTypes.DefaultConfirm
                                     && u.Account.Equals(loginQq) && u.Group.Equals(groupNo));

                        return info?.Value;
                    }
                );

            return manage;
        }
    }
}