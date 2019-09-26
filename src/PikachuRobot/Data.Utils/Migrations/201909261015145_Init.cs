namespace Data.Utils.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdiomInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Derivation = c.String(unicode: false),
                        Example = c.String(unicode: false),
                        Explanation = c.String(unicode: false),
                        Spell = c.String(unicode: false),
                        FirstSpell = c.String(unicode: false),
                        LastSpell = c.String(unicode: false),
                        Word = c.String(unicode: false),
                        Abbreviation = c.String(unicode: false),
                        CreateTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IdiomInfoes");
        }
    }
}
