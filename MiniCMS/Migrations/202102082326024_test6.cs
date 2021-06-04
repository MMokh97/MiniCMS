namespace MiniCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiteAboutUs", "FaceBook", c => c.String());
            AddColumn("dbo.SiteAboutUs", "twitter", c => c.String());
            AddColumn("dbo.SiteAboutUs", "WhatsApp", c => c.String());
            AddColumn("dbo.SiteAboutUs", "Instagram", c => c.String());
            AddColumn("dbo.SiteAboutUs", "Telegram", c => c.String());
            AddColumn("dbo.SiteAboutUs", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SiteAboutUs", "Email");
            DropColumn("dbo.SiteAboutUs", "Telegram");
            DropColumn("dbo.SiteAboutUs", "Instagram");
            DropColumn("dbo.SiteAboutUs", "WhatsApp");
            DropColumn("dbo.SiteAboutUs", "twitter");
            DropColumn("dbo.SiteAboutUs", "FaceBook");
        }
    }
}
