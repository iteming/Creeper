using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Base
{
    public class AgentLevel
    {
        public string Id { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 1)]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string IsValid { get; set; } // 是否有效

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Column(Order = 2)]
        public int AgentLevelId { get; set; }
        public string AgentLevelName { get; set; }
        public decimal DProportion { get; set; } //比例
        public decimal IProportion { get; set; } //比例
        public decimal IProportion2 { get; set; } //比例
    }
}
