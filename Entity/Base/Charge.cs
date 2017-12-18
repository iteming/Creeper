using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Base
{
    public class Charge
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int UserId { get; set; } // 充值人ID
        public string NickName { get; set; } // 充值人
        public string OrderNo { get; set; } // 订单号
        public decimal Amount { get; set; } // 充值金额
        public DateTime ChargeTime { get; set; } // 充值时间
    }
}
