using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Creeper.Web.Utils;
using Service;
using Entity.Param;
using Common.Tools;

namespace Creeper.Web.Controllers
{
    [UserAuthorFilter]
    public class RebateController : Controller
    {
        //
        // GET: /Rebate/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(int PageSize, int PageIndex)
        {
            var result = new AllService().GetRebate(new ParamUserAgent
            {
                PageIndex = PageIndex,
                PageSize = PageSize
            });

            return Content(ToolsHelper._ConvertTools.SerializeObject(result));
        }
	}
}