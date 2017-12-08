﻿using System;
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
        public ActionResult List(int PageSize, int PageIndex)
        {
            var result = new AllService().GetAgentLevel(new ParamAgentLevel
            {
                PageIndex = PageIndex,
                PageSize = PageSize
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