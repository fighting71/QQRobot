using System.Data.Entity;

namespace Pikachu.Data
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PikachuDataContext:DbContext
    {
        public PikachuDataContext()
            : base("name=PikachuContext")
        {
        }

    }
}