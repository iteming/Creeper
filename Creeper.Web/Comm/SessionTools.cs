using System.Web;
using Entity.Base;

namespace Creeper.Web.Comm
{
    public static class SessionTools
    {
        public static void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
        public static object GetSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static string UserID
        {
            get
            {
                var userid = HttpContext.Current.Session["UserID"];
                return userid == null ? "" : userid.ToString();
            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                var username = HttpContext.Current.Session["UserName"];
                return username == null ? "" : username.ToString();
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }

        public static Admin Admin
        {
            get
            {
                var admin = HttpContext.Current.Session["Admin"];
                return (Admin) admin;
            }
            set
            {
                HttpContext.Current.Session["Admin"] = value;
            }
        }
    }
}
