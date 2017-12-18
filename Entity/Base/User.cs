using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Base
{
    public class User
    {
        public string Id { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int GameId { get; set; }
        public string GameName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 2)]
        public int UserId { get; set; }
        public string NickName { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<int> MyAgentLevel { get; set; }
        public string UserType { get; set; } // 用户类型 - 普通玩家/代理
        public Nullable<DateTime> RegisterTime { get; set; }
        public Nullable<DateTime> LastLoginTime { get; set; }
        

        public Nullable<int> ParentUserId { get; set; } // 邀请关系 - 邀请人ID
        public string AgentRealName { get; set; } // 邀请关系 - 邀请人昵称
        public Nullable<DateTime> BindingTime { get; set; } // 邀请关系 - 邀请绑定时间


        public Nullable<int> FriendID { get; set; } // 好友ID
        public string FriendNickName { get; set; } // 好友昵称
        public Nullable<DateTime> FBindingTime { get; set; } //成为好友时间


        public Nullable<int> AgentLevel1 { get; set; }
        public Nullable<int> AgentLevel2 { get; set; }
        public Nullable<int> AgentLevel3 { get; set; }


        public Nullable<decimal> TotalCharge { get; set; } //总充值
        public Nullable<decimal> TodayCharge { get; set; } //今日充值
        public Nullable<decimal> TotalRound { get; set; } //总局数
        public Nullable<decimal> TodayRound { get; set; } //今日局数
        public Nullable<decimal> TotalWinRound { get; set; } //总赢得局数
        public Nullable<decimal> TodayWinRound { get; set; } //今日赢得局数
        public Nullable<decimal> Diamond { get; set; } // 开房消耗钻石

        //public Nullable<decimal> Coin { get; set; } // 金币剩余量
        //public string GameVersion { get; set; } // 游戏版本 - "1.0.0"

        public string OpenId { get; set; } // 闲雅麻将 openid
        public string UnionId { get; set; } // Unionid
        public string Wx_Openid { get; set; } // 佳之易 openid
        public string HeaderUrl { get; set; } // 头像路径

        /// <summary>
        /// 是否是本平台 已维护的用户
        /// </summary>
        public int Platform { get; set; }
    }
}
