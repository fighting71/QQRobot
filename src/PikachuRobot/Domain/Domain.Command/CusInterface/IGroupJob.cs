using System.Threading.Tasks;

namespace Domain.Command.CusInterface
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/12 16:27:30
    /// @source : 
    /// @des : 
    /// </summary>
    public interface IGroupJob
    {

        /// <summary>
        /// 获取jobId
        /// </summary>
        /// <param name="groupNo"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        string GetJobId(string groupNo, string account);

        /// <summary>
        /// 开启job
        /// </summary>
        /// <returns></returns>
        Task StartAsync(string groupNo, string account);

        /// <summary>
        /// 关闭job
        /// </summary>
        /// <returns></returns>
        Task StopAsync(string groupNo, string account);

    }
}
