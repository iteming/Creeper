namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017121020 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AgentLevels", "IsValid", c => c.String());
            AlterColumn("dbo.Products", "IsValid", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "IsValid", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AgentLevels", "IsValid", c => c.Boolean(nullable: false));
        }
    }
}
