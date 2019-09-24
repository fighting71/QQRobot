using Data.PetSystem.Models;
using System.Data.Entity;

namespace Data.PetSystem
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class PetContext : DbContext
    {
        public PetContext()
            : base("name=PetContext")
        {

        }

        public DbSet<PetInfo> PetInfos { get; set; }
        public DbSet<PetProp> PetProps { get; set; }
        public DbSet<UserPet> UserPets { get; set; }
        public DbSet<UserProp> UserProps { get; set; }
        public DbSet<PropType> PropTypes { get; set; }


    }
}