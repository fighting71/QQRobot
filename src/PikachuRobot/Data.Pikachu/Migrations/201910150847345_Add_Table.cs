namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RobotAccount = c.String(unicode: false),
                        ConfirmType = c.Byte(nullable: false),
                        CronExpression = c.String(unicode: false),
                        GroupNo = c.String(unicode: false),
                        Target = c.String(unicode: false),
                        Title = c.String(unicode: false),
                        Template = c.String(unicode: false),
                        Data = c.String(unicode: false),
                        Enable = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobConfigs");
        }
    }
}
