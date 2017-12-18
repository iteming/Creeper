namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _121217 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "OpenId", c => c.String());
            AddColumn("dbo.Users", "UnionId", c => c.String());
            AddColumn("dbo.Users", "HeaderUrl", c => c.String());
            DropColumn("dbo.Users", "Wx_Openid_XY");
            DropColumn("dbo.Users", "Wx_Unionid");
            DropColumn("dbo.Users", "Wx_HeaderUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Wx_HeaderUrl", c => c.String());
            AddColumn("dbo.Users", "Wx_Unionid", c => c.String());
            AddColumn("dbo.Users", "Wx_Openid_XY", c => c.String());
            DropColumn("dbo.Users", "HeaderUrl");
            DropColumn("dbo.Users", "UnionId");
            DropColumn("dbo.Users", "OpenId");
        }
    }
}
