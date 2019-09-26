using Data.PetSystem;
using Data.PetSystem.Models;
using System.Linq;

namespace Services.PetSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/24 11:50:51
    /// @source : 
    /// @des : 
    /// </summary>
    public class PetService : BaseService
    {
        public PetService(PetContext context) : base(context)
        {
        }

        public PetInfo GetInfoByName (string name)
        {
            return PetContext.PetInfos.FirstOrDefault(u => u.Enable && u.Name.Equals(name));
        }

        public IQueryable<PetInfo> GetAll()
        {
            return PetContext.PetInfos.Where(u => u.Enable);
        }

    }
}
