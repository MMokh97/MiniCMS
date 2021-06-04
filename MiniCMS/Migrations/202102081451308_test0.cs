namespace MiniCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        UserId = c.String(maxLength: 128),
                        LangId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.LangId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LangId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteImages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SiteImages", "LangId", "dbo.Languages");
            DropIndex("dbo.SiteImages", new[] { "LangId" });
            DropIndex("dbo.SiteImages", new[] { "UserId" });
            DropTable("dbo.SiteImages");
        }
    }
}
