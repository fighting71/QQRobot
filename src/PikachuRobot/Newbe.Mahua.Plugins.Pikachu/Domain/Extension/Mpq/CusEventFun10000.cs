using Newbe.Mahua.Commands;
using System.Collections.Generic;
using Newbe.Mahua.MPQ;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.Extension.Mpq
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/14 10:56:36
    /// @source : 
    /// @des : 
    /// </summary>
    public class CusEventFun10000 : IEventFun
    {
        private readonly IEnumerable<ISystemInitEvent> _systemInitEvents;

        public CusEventFun10000(
            IEnumerable<ISystemInitEvent> systemInitEvents)
        {
            _systemInitEvents = systemInitEvents;
        }

        public int EventFun { get; } = 10000;

        public void Handle(EventFunInput eventFunInput)
        {
            _systemInitEvents.Handle(x => x.Handle());
        }
    }
}
