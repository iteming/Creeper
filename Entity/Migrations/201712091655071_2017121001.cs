namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017121001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Agents", "Id", c => c.String());
            AddColumn("dbo.AgentLevels", "Id", c => c.String());
            AddColumn("dbo.Users", "Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Id");
            DropColumn("dbo.AgentLevels", "Id");
            DropColumn("dbo.Agents", "Id");
        }
    }
}
