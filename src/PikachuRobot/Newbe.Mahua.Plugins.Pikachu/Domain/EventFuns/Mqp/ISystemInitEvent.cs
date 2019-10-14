using System.Threading.Tasks;
using Newbe.Mahua.MPQ;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.EventFuns.Mqp
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/14 10:28:37
    /// @source : 
    /// @des : 
    /// </summary>
    public interface ISystemInitEvent : IMahuaEvent
    {

        Task Handle();

    }
}
