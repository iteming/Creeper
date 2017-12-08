using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    public class ToolsHelper
    {
        /// <summary>
        /// 类型转化助手类
        /// </summary>
        public static ConvertTools _ConvertTools = ConvertTools._ConvertTools;
        
        /// <summary>
        /// 距离测算助手类
        /// </summary>
        public static DistHelperTools _DistHelperTools = DistHelperTools._DistHelperTools;
       
        /// <summary>
        /// 正则助手类
        /// </summary>
        public static RegulerHelper _RegulerHelper = RegulerHelper._RegulerHelper;

        public static CookieHelper _CookieHelper =  CookieHelper._CookieHelper;

        public static HttpHelper _HttpHelper = HttpHelper._HttpHelper;
    }
}
