namespace MiniCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SitePartners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
            DropForeignKey("dbo.SitePartners", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SitePartners", "LangId", "dbo.Languages");
            DropIndex("dbo.SitePartners", new[] { "LangId" });
            DropIndex("dbo.SitePartners", new[] { "UserId" });
            DropTable("dbo.SitePartners");
        }
    }
}
