using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Utils;

namespace Services.Utils
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/26 17:42:46
    /// @source : 
    /// @des : 
    /// </summary>
    public class IdiomsService : BaseService
    {
        public IdiomsService(UtilsContext utilsContext) : base(utilsContext)
        {
        }

        public int GetCount()
        {
            return UtilsContext.IdiomInfos.Count();
        }

        public Data.Utils.Models.IdiomInfo GetInfo(int id)
        {
            return UtilsContext.IdiomInfos.FirstOrDefault(u => u.Id == id);
        }

        public Data.Utils.Models.IdiomInfo GetInfo(string word)
        {
            return UtilsContext.IdiomInfos.FirstOrDefault(u => u.Word == word);
        }

    }
}
