namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_Change : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "ActivityLogs", newName: "GroupActivities");
        }
        
        public override void Down()
        {
            RenameTable(name: "GroupActivities", newName: "ActivityLogs");
        }
    }
}
