using System.Web.Mvc;
using Creeper.Web.Utils;
using Entity.Param;
using Service;

namespace Creeper.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Login()
        {
            if (SessionTools.Admin != null)
                return Redirect("/Home/Index");

            return View(new Entity.Base.Admin());
        }
        
        [HttpPost]
        public ActionResult Login(ParamLogin model)
        {
            var result = new AllService().Login(model);
            if (result != null && result.Ret > 0)
            {
                SessionTools.Admin = result.Data;
                return Redirect("/Home/Index");
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(new Entity.Base.Admin());
        }

        public ActionResult Logout()
        {
            SessionTools.Admin = null;
            return Redirect("/Home/Index");
        }
        
        public ActionResult Index()
        {
            return View();
        }
    }
}