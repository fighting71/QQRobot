using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Utils;
using Data.Utils.Models;

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

        public Task<int> GetCountAsync()
        {
            return UtilsContext.IdiomInfos.CountAsync();
        }
        
        public IdiomInfo GetInfo(int id)
        {
            return UtilsContext.IdiomInfos.FirstOrDefault(u => u.Id == id);
        }

        public Task<IdiomInfo> GetInfoAsync(int id)
        {
            return UtilsContext.IdiomInfos.FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public IdiomInfo GetInfo(string word)
        {
            return UtilsContext.IdiomInfos.FirstOrDefault(u => u.Word == word);
        }

        public Task<IdiomInfo> GetByWordAsync(string word)
        {
            return UtilsContext.IdiomInfos.FirstOrDefaultAsync(u => u.Word == word);
        }
        
    }
}
