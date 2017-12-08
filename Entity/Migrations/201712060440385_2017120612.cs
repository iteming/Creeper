namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017120612 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AgentLevels", "IProportion3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AgentLevels", "IProportion3", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
