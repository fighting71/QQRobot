using Autofac;
using System.Threading.Tasks;

namespace Newbe.Mahua.Plugins.Pikachu.Domain.CusInterface
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/10 15:37:56
    /// @source : 
    /// @des : 
    /// </summary>
    public interface IWebHost
    {
        /// <summary>
        /// 启动Web服务
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="lifetimeScope"></param>
        Task StartAsync(string baseUrl, ILifetimeScope lifetimeScope);

        /// <summary>
        /// 停止
        /// </summary>
        Task StopAsync();
    }
}
