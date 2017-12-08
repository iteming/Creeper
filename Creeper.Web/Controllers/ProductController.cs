using System.Web.Mvc;
using Creeper.Web.Comm;
using Service;
using Entity.Param;
using Common.Tools;

namespace Creeper.Web.Controllers
{
    [UserAuthorFilter]
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(int PageSize, int PageIndex)
        {
            var result = new AllService().GetProduct(new ParamProduct
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            });

            return Content(ToolsHelper._ConvertTools.SerializeObject(result));
        }
    }
}