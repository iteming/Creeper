using System.Web;
using System.Web.Mvc;
using Creeper.WX.Utils;

namespace Creeper.WX
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserAuthorFilterAttribute());
        }
    }
}
