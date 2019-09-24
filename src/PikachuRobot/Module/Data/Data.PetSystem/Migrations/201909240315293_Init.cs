namespace Data.PetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PetInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        InitLevel = c.Int(nullable: false),
                        Price = c.Long(nullable: false),
                        Face = c.String(unicode: false),
                        Attack = c.Int(nullable: false),
                        Intellect = c.Int(nullable: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PetProps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        PropType = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Ext = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        Ext = c.String(unicode: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PetId = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Sex = c.Byte(nullable: false),
                        Face = c.String(unicode: false),
                        Owner = c.String(unicode: false),
                        Healthy = c.Int(nullable: false),
                        Mood = c.Int(nullable: false),
                        Attack = c.Int(nullable: false),
                        Intellect = c.Int(nullable: false),
                        Quality = c.Int(nullable: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropId = c.Int(nullable: false),
                        PropNum = c.Long(nullable: false),
                        Owner = c.String(unicode: false),
                        CreateTime = c.DateTime(precision: 0),
                        UpdateTime = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProps");
            DropTable("dbo.UserPets");
            DropTable("dbo.PropTypes");
            DropTable("dbo.PetProps");
            DropTable("dbo.PetInfoes");
        }
    }
}
