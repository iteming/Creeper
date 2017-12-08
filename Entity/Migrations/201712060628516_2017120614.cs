namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017120614 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Charges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        GameName = c.String(),
                        SourceOrderId = c.String(),
                        OrderId = c.String(),
                        PaymentId = c.String(),
                        OrderState = c.Int(nullable: false),
                        IsReceive = c.Int(nullable: false),
                        ChargeUserId = c.Int(nullable: false),
                        NickName = c.String(),
                        UserId = c.Int(nullable: false),
                        RealName = c.String(),
                        RebateAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChargeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Writedate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Charges");
        }
    }
}
