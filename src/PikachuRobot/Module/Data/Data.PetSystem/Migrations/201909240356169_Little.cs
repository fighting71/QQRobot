namespace Data.PetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Little : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PetInfoes", "Enable", c => c.Boolean(nullable: false));
            AddColumn("dbo.PetProps", "Enable", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserPets", "Enable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPets", "Enable");
            DropColumn("dbo.PetProps", "Enable");
            DropColumn("dbo.PetInfoes", "Enable");
        }
    }
}
