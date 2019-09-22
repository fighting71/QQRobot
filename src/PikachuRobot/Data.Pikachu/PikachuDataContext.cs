using Data.Pikachu.Models;
using System.Data.Entity;

namespace Data.Pikachu
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PikachuDataContext:DbContext
    {
        public PikachuDataContext()
            : base("name=PikachuContext")
        {

        }

        public DbSet<ConfigInfo> configInfos { get; set; }

        public DbSet<Manager> managers { get; set; }


    }
}