namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Little : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigInfoes", "Enable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigInfoes", "Enable");
        }
    }
}
