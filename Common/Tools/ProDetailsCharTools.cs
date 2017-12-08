using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Creeper.Tools
{
    /// <summary>
    /// 项目详情字符转换
    /// </summary>
    public static class ProDetailsCharTools
    {
        public static string GetHtmlFormat(string Details) {
            try
            {
                return SetHtml(HttpUtility.HtmlDecode(Details));
            }
            catch (Exception)
            {
                return SetHtml("转译出错！");
            }
        }

        private static string SetHtml(string Details)
        {
            return "<html><body>" + Details + "</body></html>";
        }
    }
}
