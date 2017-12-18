using System.Web.Mvc;

namespace Creeper.Web.Utils
{
    public class UserAuthorFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string returnURL = filterContext.HttpContext.Request.Url.AbsolutePath;
            if (SessionTools.Admin == null)
            {
                filterContext.HttpContext.Response.Redirect("/Account/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}