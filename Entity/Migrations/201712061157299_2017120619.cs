namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017120619 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Charges", "Platform", c => c.Int(nullable: false));
            AddColumn("dbo.Charges", "OldSourceOrderId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Charges", "OldSourceOrderId");
            DropColumn("dbo.Charges", "Platform");
        }
    }
}
