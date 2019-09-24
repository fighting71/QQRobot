using Autofac;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Commond.Tools;
using GenerateMsg;
using Newbe.Mahua.Plugins.Pikachu.Domain.Manage;
using Data.Pikachu;
using GenerateMsg.PrivateMsg;
using Newbe.Mahua.Plugins.Pikachu.Domain.CusConst;
using StackExchange.Redis;
using IServiceSupply;
using GenerateMsg.Services;

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
        private static readonly Logger _logger = LogManager.GetLogger(nameof(MahuaModule));

        private static ConnectionMultiplexer ctx = ConnectionMultiplexer.Connect(ConfigConst.RedisClient);

        public InitModule()
        {
            _logger.Debug("开始进行初始化");
        }


        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PikachuDataContext>();// 此处若是注入单例 会引起 context 随着依赖的对象释放而释放
            builder.RegisterType<PrivateMsgManage>().As<IPrivateMsgDeal>();
            builder.RegisterType<GroupMsgManage>().As<IGroupMsgDeal>();

            try
            {
                //InitPrivateMsgManage();

                //InitGroupMsgManage();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private void InitPrivateMsgManage()
        {
            int.TryParse(ConfigConst.RedisDb, out var db);

            PrivateMsgManage.AddDeal(new ConfigCacheDeal(
                ctx.GetDatabase(db), 
                InstanceFactory.Get(()=>new ConfigService(InstanceFactory.Get<PikachuDataContext>()))
                ).Run);

            PrivateMsgManage.AddDeal(new ConfigDeal(
                ctx.GetDatabase(db),
                InstanceFactory.Get(() => new ConfigService(InstanceFactory.Get<PikachuDataContext>()))
                ).Run);

            PrivateMsgManage.AddDeal(new GroupManageDeal(InstanceFactory.Get<PikachuDataContext>()).Run);

            PrivateMsgManage.AddDeal(new GroupMsgCopyDeal(InstanceFactory.Get<PikachuDataContext>()).Run);

            PrivateMsgManage.AddDeal((context, api) =>
                {
                    return InstanceFactory.Get<PikachuDataContext>().ConfigInfos.FirstOrDefault(u => u.Enable && u.Key.Equals("Private.Confirm.Default"))?.Value;
                }
            );

        }


        private void InitGroupMsgManage()
        {

            //GroupMsgManage.AddDeal(new NoticeDeal().Run);
            //GroupMsgManage.AddDeal((context,api)=>
            //{
            //    if (!string.IsNullOrWhiteSpace(context.Message))
            //    {
            //        return context.Message;
            //    }
            //    return string.Empty;
            //}); // 复读

            GroupMsgManage.AddDeal((context, api) =>
               {
                   return InstanceFactory.Get<PikachuDataContext>().ConfigInfos.FirstOrDefault(u => u.Enable && u.Key.Equals("Group.Confirm.Default"))?.Value;
               }
            );
        }
    }
}