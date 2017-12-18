namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _121116 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        RoleName = c.String(),
                        RoleId = c.Int(nullable: false),
                        IsPrimary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Id = c.String(),
                        GameName = c.String(),
                        RealName = c.String(),
                        NickName = c.String(),
                        PhoneNo = c.String(),
                        MyAgentLevel = c.Int(),
                        AgentLevelName = c.String(),
                        ParentUserId = c.Int(),
                        AgentLevel1 = c.Int(),
                        AgentLevel2 = c.Int(),
                        AgentCode = c.String(),
                        AgentStatus = c.String(),
                        CreateTime = c.DateTime(),
                        Platform = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.UserId });
            
            CreateTable(
                "dbo.AgentApplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        GameName = c.String(),
                        UserId = c.String(),
                        RealName = c.String(),
                        NickName = c.String(),
                        PhoneNo = c.String(),
                        RegisterTime = c.DateTime(),
                        GameRounds = c.Int(),
                        RoomCardUsed = c.Int(),
                        ApplyTime = c.DateTime(),
                        AuditTime = c.DateTime(),
                        PassFlag = c.Int(nullable: false),
                        Remark = c.String(),
                        Platform = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AgentLevels",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        AgentLevelId = c.Int(nullable: false),
                        Id = c.String(),
                        GameName = c.String(),
                        IsValid = c.String(),
                        AgentLevelName = c.String(),
                        DProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IProportion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IProportion2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.GameId, t.AgentLevelId });
            
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
                        Platform = c.Int(nullable: false),
                        OldSourceOrderId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        GameName = c.String(),
                        IsValid = c.String(),
                        Remark = c.String(),
                        ServerApiUrl = c.String(),
                        DBWanIP = c.String(),
                        DBIntranetIP = c.String(),
                        DBProt = c.String(),
                    })
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Id = c.String(),
                        GameName = c.String(),
                        NickName = c.String(),
                        PhoneNo = c.String(),
                        MyAgentLevel = c.Int(),
                        UserType = c.String(),
                        RegisterTime = c.DateTime(),
                        LastLoginTime = c.DateTime(),
                        ParentUserId = c.Int(),
                        AgentRealName = c.String(),
                        BindingTime = c.DateTime(),
                        FriendID = c.Int(),
                        FriendNickName = c.String(),
                        FBindingTime = c.DateTime(),
                        AgentLevel1 = c.Int(),
                        AgentLevel2 = c.Int(),
                        AgentLevel3 = c.Int(),
                        TotalCharge = c.Decimal(precision: 18, scale: 2),
                        TodayCharge = c.Decimal(precision: 18, scale: 2),
                        TotalRound = c.Decimal(precision: 18, scale: 2),
                        TodayRound = c.Decimal(precision: 18, scale: 2),
                        TotalWinRound = c.Decimal(precision: 18, scale: 2),
                        TodayWinRound = c.Decimal(precision: 18, scale: 2),
                        Diamond = c.Decimal(precision: 18, scale: 2),
                        Wx_Openid_XY = c.String(),
                        Wx_Openid = c.String(),
                        Wx_Unionid = c.String(),
                        Wx_HeaderUrl = c.String(),
                        Platform = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.UserId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Charges");
            DropTable("dbo.AgentLevels");
            DropTable("dbo.AgentApplies");
            DropTable("dbo.Agents");
            DropTable("dbo.Admins");
        }
    }
}
