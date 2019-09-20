using System;
using NLog;

namespace Newbe.Mahua.Plugins.Pikachu.CusTools
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/19 14:40:10
    /// @source : 
    /// @des : 
    /// </summary>
    public static class CatchTools
    {
        private static Logger _logger = LogManager.GetLogger(nameof(CatchTools));

        public static void TryRun(this Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }
        
    }
}