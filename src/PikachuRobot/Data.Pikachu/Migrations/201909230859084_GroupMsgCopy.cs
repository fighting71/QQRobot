namespace XD.Store.Model.Log.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupMsgCopy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupMsgCopies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromGroup = c.String(unicode: false),
                        TargetGroup = c.String(unicode: false),
                        Person = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GroupMsgCopies");
        }
    }
}
