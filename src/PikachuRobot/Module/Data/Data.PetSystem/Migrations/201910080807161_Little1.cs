namespace Data.PetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Little1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPets", "Name", c => c.String(unicode: false));
            AddColumn("dbo.UserPets", "NickName", c => c.String(unicode: false));
            AddColumn("dbo.UserPets", "Account", c => c.String(unicode: false));
            AddColumn("dbo.UserPets", "Group", c => c.String(unicode: false));
            DropColumn("dbo.UserPets", "Owner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPets", "Owner", c => c.String(unicode: false));
            DropColumn("dbo.UserPets", "Group");
            DropColumn("dbo.UserPets", "Account");
            DropColumn("dbo.UserPets", "NickName");
            DropColumn("dbo.UserPets", "Name");
        }
    }
}
