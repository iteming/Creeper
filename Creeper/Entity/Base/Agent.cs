using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Base
{
    public class Agent
    {
        public string Id { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int GameId { get; set; }
        public string GameName { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 2)]
        public int UserId { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<int> MyAgentLevel { get; set; }
        public string AgentLevelName { get; set; }
        public Nullable<int> ParentUserId { get; set; }
        public Nullable<int> AgentLevel1 { get; set; }
        public Nullable<int> AgentLevel2 { get; set; }
        //public Nullable<int> AgentLevel3 { get; set; }

        public string AgentCode { get; set; }
        public string AgentStatus { get; set; }
        public Nullable<DateTime> CreateTime { get; set; }


        //public Nullable<decimal> DProportion { get; set; } //比例
        //public Nullable<decimal> IProportion { get; set; } //比例
        //public Nullable<decimal> IProportion2 { get; set; } //比例

        /// <summary>
        /// 是否是本平台 已维护的代理/推广员
        /// </summary>
        public int Platform { get; set; }
    }
}
