namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017120718 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agents", "Platform", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Platform", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Platform");
            DropColumn("dbo.Agents", "Platform");
        }
    }
}
