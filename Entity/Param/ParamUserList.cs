using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Param
{
    public class ParamUserList : ParamPager
    {
        /// <summary>
        /// 代理级别
        /// </summary>
        public int? AgentLevel { get; set; }
        /// <summary>
        /// 0 , 1 , 99
        /// </summary>
        public int? Type { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
    }
}
