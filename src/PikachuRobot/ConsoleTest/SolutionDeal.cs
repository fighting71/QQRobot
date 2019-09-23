using Data.Pikachu;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/9/23 14:22:24
    /// @source : 
    /// @des : 
    /// </summary>
    public class SolutionDeal
    {

        private IDatabase database;

        private PikachuDataContext dbContext;

        public SolutionDeal(IDatabase database, PikachuDataContext dbContext)
        {
            this.database = database;
            this.dbContext = dbContext;
        }
    }
}
