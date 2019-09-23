namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupAuths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupNo = c.String(unicode: false),
                        Enable = c.Boolean(nullable: false),
                        Level = c.Int(nullable: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GroupAuths");
        }
    }
}
