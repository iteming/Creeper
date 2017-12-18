namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17121511 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rebates",
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
                        Platform = c.Int(nullable: false),
                        OldSourceOrderId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Charges", "OrderNo", c => c.String());
            AddColumn("dbo.Charges", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Charges", "ChargeTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Charges", "SourceOrderId");
            DropColumn("dbo.Charges", "OrderId");
            DropColumn("dbo.Charges", "PaymentId");
            DropColumn("dbo.Charges", "OrderState");
            DropColumn("dbo.Charges", "IsReceive");
            DropColumn("dbo.Charges", "ChargeUserId");
            DropColumn("dbo.Charges", "RealName");
            DropColumn("dbo.Charges", "RebateAmount");
            DropColumn("dbo.Charges", "ChargeAmount");
            DropColumn("dbo.Charges", "DProportion");
            DropColumn("dbo.Charges", "Writedate");
            DropColumn("dbo.Charges", "Platform");
            DropColumn("dbo.Charges", "OldSourceOrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Charges", "OldSourceOrderId", c => c.String());
            AddColumn("dbo.Charges", "Platform", c => c.Int(nullable: false));
            AddColumn("dbo.Charges", "Writedate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Charges", "DProportion", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Charges", "ChargeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Charges", "RebateAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Charges", "RealName", c => c.String());
            AddColumn("dbo.Charges", "ChargeUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Charges", "IsReceive", c => c.Int(nullable: false));
            AddColumn("dbo.Charges", "OrderState", c => c.Int(nullable: false));
            AddColumn("dbo.Charges", "PaymentId", c => c.String());
            AddColumn("dbo.Charges", "OrderId", c => c.String());
            AddColumn("dbo.Charges", "SourceOrderId", c => c.String());
            DropColumn("dbo.Charges", "ChargeTime");
            DropColumn("dbo.Charges", "Amount");
            DropColumn("dbo.Charges", "OrderNo");
            DropTable("dbo.Rebates");
        }
    }
}
