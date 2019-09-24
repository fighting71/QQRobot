using Data.PetSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateMsg.Services
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 11:50:51
    /// @source : 
    /// @des : 
    /// </summary>
    public class PetService
    {

        private PetContext _petContext;
        public PetService(PetContext petContext)
        {
            _petContext = petContext;
        }

        public IQueryable<Data.PetSystem.Models.PetInfo> GetAll()
        {
            return _petContext.PetInfos.Where(u => u.Enable);
        }

    }
}
