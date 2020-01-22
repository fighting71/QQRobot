using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningMan.Data
{
    /// <summary>
    /// @auth : monster
    /// @since : 2019/10/17 11:32:14
    /// @source : 
    /// @des : 
    /// </summary>
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class CoreContext : DbContext
    {

        public CoreContext()
            : base("name=RunningManContext")
        {

        }

    }
}
