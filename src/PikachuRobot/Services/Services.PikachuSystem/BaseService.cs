using Data.Pikachu;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 13:54:13
    /// @source : 
    /// @des : 
    /// </summary>
    public class BaseService
    {

        public BaseService(PikachuDataContext pikachuDataContext)
        {
            PikachuDataContext = pikachuDataContext;
        }

        protected PikachuDataContext PikachuDataContext { get; }
    }
}
