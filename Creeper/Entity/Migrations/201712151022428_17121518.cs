namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17121518 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rebates", "RebateType", c => c.Int(nullable: false));
            AddColumn("dbo.Rebates", "OrderNo", c => c.String());
            DropColumn("dbo.Rebates", "OldSourceOrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rebates", "OldSourceOrderId", c => c.String());
            DropColumn("dbo.Rebates", "OrderNo");
            DropColumn("dbo.Rebates", "RebateType");
        }
    }
}
