using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Param
{
    public class ParamAgentLevel : ParamPager
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int AgentLevelId { get; set; }
        public string AgentLevelName { get; set; }
    }
}
