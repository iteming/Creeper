using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Creeper.Web.Comm;
using Service;
using Entity.Param;
using Common.Tools;
using Entity.Base;

namespace Creeper.Web.Controllers
{
    [UserAuthorFilter]
    public class AgentLevelController : Controller
    {
        //
        // GET: /AgentLevel/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(ParamAgentLevel param)
        {
            var result = new AllService().GetAgentLevel(new ParamAgentLevel
            {
                keywords = param.keywords,
                PageIndex = param.page,
                PageSize = param.rows
            });
            return Content(ToolsHelper._ConvertTools.SerializeObject(result));
        }

        public ActionResult Detail(string id)
        {
            var result = new AllService().GetAgentLevelByid(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult Update(AgentLevel entity)
        {
            entity.IsValid = entity.IsValid == "on" ? "1" : "0";
            var result = new AllService().UpdateAgentLevel(entity);
            if (result.Ret == 1)
                return Redirect("/AgentLevel/Index");
            return Content(ToolsHelper._ConvertTools.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult Delete(List<string> ids)
        {
            var result = new AllService().DeleteAgentLevel(ids);
            return Content(ToolsHelper._ConvertTools.SerializeObject(result));
        }
	}
}