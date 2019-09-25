namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Little3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(unicode: false),
                        Value = c.String(unicode: false),
                        GetGroupConfigType = c.Byte(nullable: false),
                        Group = c.String(unicode: false),
                        Enable = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GroupConfigs");
        }
    }
}
