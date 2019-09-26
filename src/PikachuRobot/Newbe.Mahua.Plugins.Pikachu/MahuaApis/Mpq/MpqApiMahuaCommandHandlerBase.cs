using MediatR;
using Newbe.Mahua.Apis;
using Newbe.Mahua.Commands;
using Newbe.Mahua.NativeApi;

namespace Newbe.Mahua.Plugins.Pikachu.MahuaApis.Mpq
{

    public interface IEventFunOutput
    {
        int Result { get; set; }
    }

    public class EventFunOutput : IEventFunOutput
    {
        public int Result { get; set; } = 0;
    }

    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 10:00:30
    /// @source : 
    /// @des : 
    /// </summary>
    public abstract class MpqApiMahuaCommandHandlerBase<TCmd, TResult> : IApiCommandHandler<TCmd, TResult>, ICommandHandler<TCmd, TResult>, IRequestHandler<TCmd, TResult>, ICommandHandler, IApiCommandHandler
    where TCmd : ApiMahuaCommand<TResult>
    where TResult : ApiMahuaCommandResult
    {
        private readonly IRobotSessionContext _robotSessionContext;
        private readonly IEventFunOutput _eventFunOutput;

        public IMpqApi MpqApi { get; }

        public int ResultCode
        {
            get
            {
                return this._eventFunOutput.Result;
            }
            set
            {
                this._eventFunOutput.Result = value;
            }
        }

        public string CurrentQq
        {
            get
            {
                return this._robotSessionContext.CurrentQq;
            }
        }

        protected MpqApiMahuaCommandHandlerBase(
          IMpqApi mpqApi,
          IRobotSessionContext robotSessionContext,
          IEventFunOutput eventFunOutput)
        {
            this._robotSessionContext = robotSessionContext;
            this._eventFunOutput = eventFunOutput;
            this.MpqApi = mpqApi;
        }

        public abstract TResult Handle(TCmd message);
    }
}
