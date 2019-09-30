namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Little1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActivityLogs", "Description", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActivityLogs", "Description");
        }
    }
}
