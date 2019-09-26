using Data.PetSystem;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PetSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/25 13:54:13
    /// @source : 
    /// @des : 
    /// </summary>
    public class BaseService
    {

        public BaseService(PetContext petContext)
        {
            PetContext = petContext;
        }

        protected PetContext PetContext { get; }
    }
}
