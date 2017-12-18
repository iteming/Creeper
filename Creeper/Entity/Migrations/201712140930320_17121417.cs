namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17121417 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgentLevels", "IProportion3", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AgentLevels", "IProportion3");
        }
    }
}
