namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2017120720 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AgentApplies");
        }
    }
}
