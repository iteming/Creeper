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

        /// <summary>
        /// _search=false&nd=1512731833438&rows=20&page=1&sidx=&sord=asc
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List(bool _search, string nd, int rows, int page,string sidx, string sord)
        {
            var result = new AllService().GetAgentLevel(new ParamAgentLevel
            {
                PageIndex = page,
                PageSize = rows
            });

            return Content(ToolsHelper._ConvertTools.SerializeObject(result));
        }

        [HttpPost]
        public ActionResult Update(string jsonData)
        {
            var entity = ToolsHelper._ConvertTools.DeserializeObject<AgentLevel>(jsonData);
            var result = new AllService().UpdateAgentLevel(entity);
            return Content(ToolsHelper._ConvertTools.SerializeObject(result));
        }
	}
}