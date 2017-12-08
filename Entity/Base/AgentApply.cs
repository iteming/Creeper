using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Base
{
    public class AgentApply
    {
        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }
        public string GameName { get; set; }
        public string UserId { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<DateTime> RegisterTime { get; set; }
        public Nullable<int> GameRounds { get; set; } // 游戏局数
        public Nullable<int> RoomCardUsed { get; set; } // 钻石消耗
        public Nullable<DateTime> ApplyTime { get; set; } // 申请代理时间
        public Nullable<DateTime> AuditTime { get; set; } // 审核时间
        public int PassFlag { get; set; } // 审核状态（0：未审核，1：已通过，2：已拒绝）
        public string Remark { get; set; } // 备注（1：审核成功，2：5天到期自动删除/审核失败）

        /// <summary>
        /// 是否是本平台 已维护的申请
        /// </summary>
        public int Platform { get; set; }
    }
}
