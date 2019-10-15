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

        public DbSet<ConfigInfo> ConfigInfos { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<GroupAuth> GroupAuths { get; set; }

        public DbSet<GroupMsgCopy> GroupMsgCopys { get; set; }

        public DbSet<GroupConfig> GroupConfigs { get; set; }

        public DbSet<MemberInfo> MemberInfos { get; set; }

        public DbSet<BillFlow> BillFlows { get; set; }

        public DbSet<GroupActivity> GroupActivities { get; set; }

        public DbSet<JobConfig> JobConfigs { get; set; }

    }
}