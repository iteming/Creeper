using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Base
{
    public class Charge
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }

        public string SourceOrderId { get; set; } // 订单号
        public string OrderId { get; set; } // 商户订单号
        public string PaymentId { get; set; } // 微信支付流水号

        public int OrderState { get; set; } // 是否领取
        public int IsReceive { get; set; } // 是否领取

        public int ChargeUserId { get; set; } // 充值人ID
        public string NickName { get; set; } // 充值人

        public int UserId { get; set; } // 被返利人ID
        public string RealName { get; set; }  // 被返利人

        public decimal RebateAmount { get; set; } // 返利金额
        public decimal ChargeAmount { get; set; } // 充值金额
        public decimal DProportion { get; set; } // 返利比例
        public DateTime Writedate { get; set; } // 充值时间

        /// <summary>
        /// 是否是本平台返利的订单
        /// </summary>
        public int Platform { get; set; }
        /// <summary>
        /// 原平台返利订单号
        /// </summary>
        public string OldSourceOrderId { get; set; } 
    }
}
