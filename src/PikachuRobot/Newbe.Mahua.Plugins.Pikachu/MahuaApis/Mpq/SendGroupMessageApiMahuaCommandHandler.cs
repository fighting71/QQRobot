using Newbe.Mahua.Apis;
using Newbe.Mahua.NativeApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newbe.Mahua.MahuaEvents;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaApis.Mpq
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/12 14:44:00
    /// @source : 
    /// @des : 
    /// </summary>
    public class SendGroupMessageApiMahuaCommandHandler : MpqApiMahuaCommandHandlerBase<SendGroupMessageApiMahuaCommand, SendGroupMessageApiMahuaCommandResult>
    {
        public SendGroupMessageApiMahuaCommandHandler(IMpqApi mpqApi, IRobotSessionContext robotSessionContext, IEventFunOutput eventFunOutput) : base(mpqApi, robotSessionContext, eventFunOutput)
        {
        }

        public override SendGroupMessageApiMahuaCommandResult Handle(SendGroupMessageApiMahuaCommand message)
        {
IInitializationMahuaEvent
            // 需传递发送人
            var res = MpqApi.Api_SendMsg(null, 2, 0, message.ToGroup, null, message.Message);

            return null;

        }
    }
}
