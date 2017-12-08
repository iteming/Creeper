using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Base
{
    public class Level
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AgentLevelId { get; set; }
        public string AgentLevelName { get; set; }
        public decimal DProportion { get; set; } //比例
        public decimal IProportion { get; set; } //比例
        public decimal IProportion2 { get; set; } //比例
    }
}
