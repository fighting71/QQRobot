using Data.Utils.Models;
using System.Data.Entity;

namespace Data.Utils
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class UtilsContext:DbContext
    {
        public UtilsContext()
            : base("name=UtilsContext")
        {

        }

        public DbSet<IdiomInfo> IdiomInfos { get; set; }

    }
}