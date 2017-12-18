using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Param
{
    public class ParamUserAgent : ParamPager
    {
        public int GameId { get; set; }
        public string GameName { get; set; }

        /// <summary>
        /// UserId、RealName、NickName、PhoneNo
        /// </summary>
        public string UserKey { get; set; }

        /// <summary>
        /// UserId、RealName、NickName、PhoneNo
        /// </summary>
        public string OrtherUserKey { get; set; }

        public int MyAgentLevel { get; set; }

        public bool searchPromoter { get; set; }
        public bool thisPlatform { get; set; }
    }
}
