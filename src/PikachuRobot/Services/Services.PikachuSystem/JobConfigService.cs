using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Pikachu;
using Data.Pikachu.Models;

namespace Services.PikachuSystem
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/15 16:13:41
    /// @source : 
    /// @des : 
    /// </summary>
    public class JobConfigService : BaseService
    {
        public JobConfigService(PikachuDataContext pikachuDataContext) : base(pikachuDataContext)
        {
        }

        public Task<List<JobConfig>> GetListAsync()
        {
            return PikachuDataContext.JobConfigs.Where(u => u.Enable).ToListAsync();
        }

        public Task<JobConfig> GetAsync(int id)
        {
            return PikachuDataContext.JobConfigs.FirstOrDefaultAsync(u=>u.Enable && u.Id == id);
        }
        
    }
}