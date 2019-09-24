namespace Data.PetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Little2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PetInfoes", "Description", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PetInfoes", "Description");
        }
    }
}
