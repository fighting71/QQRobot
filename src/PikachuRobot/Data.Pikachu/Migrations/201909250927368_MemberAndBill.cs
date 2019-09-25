namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberAndBill : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillFlows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Group = c.String(unicode: false),
                        Account = c.String(unicode: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillType = c.Byte(nullable: false),
                        Description = c.String(unicode: false),
                        Enable = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MemberInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Group = c.String(unicode: false),
                        Account = c.String(unicode: false),
                        Level = c.Int(nullable: false),
                        Enable = c.Boolean(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(unicode: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MemberInfoes");
            DropTable("dbo.BillFlows");
        }
    }
}
