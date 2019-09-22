namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfigInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, unicode: false),
                        Value = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, unicode: false),
                        Flag = c.String(unicode: false),
                        Enable = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Managers");
            DropTable("dbo.ConfigInfoes");
        }
    }
}
