
using Data.Utils;

namespace Services.Utils
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 17:42:46
    /// @source : 
    /// @des : 
    /// </summary>
    public class BaseService
    {

        public BaseService(UtilsContext utilsContext)
        {
            UtilsContext = utilsContext;
        }

        protected UtilsContext UtilsContext { get; }
    }
}
