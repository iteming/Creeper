using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Common.Tools;
using Entity.Dto;
using Entity.Param;
using Service;

namespace Creeper.WX.Controllers
{
    public class PromoterController : Controller
    {
        //
        // GET: /Promoter/

        /// <summary>
        /// 推广员管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var entity = new WxService().GetUserById(SessionHelper._SessionHelper.UserID);
            return View(entity);
        }

        public ActionResult Main()
        {
            var user = new WxService().GetUserById(SessionHelper._SessionHelper.UserID);
            var agent = new WxService().GetAgentById(SessionHelper._SessionHelper.UserID);
            var mua = new DtoRebateStatistics { user = user, agent = agent };
            if (user != null)
            {
                new WxService().GetRebateStatistics(user.GameId, user.UserId, ref mua);
                new WxService().GetUserCountStatistics(user.GameId, user.UserId, ref mua);
            }
            return View(mua);
        }

        public ActionResult UserList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserListGet(ParamUserList param)
        {
            var result = new WxService().GetUserCountList(SessionHelper._SessionHelper.UserID, param);
            if (result.Ret > 0)
            {
                var listBoxHTML = new StringBuilder();
                int i = 1;
                foreach (var user in result.Data)
                {
                    listBoxHTML.Append(string.Format(@"<div class='mui-table-view-cell rz tr {3}'>
	                        <div class='mui-pull-left cz cz1 mui-ellipsis'>{0}</div>
	                        <div class='mui-pull-left cz cz2'>{1}</div>
	                        <div class='mui-pull-left cz cz3'>{2}</div>
	                        <div class='mui-pull-left cz cz4'><div></div></div></div>",
                            user.NickName, user.UserId, user.BindingTime, i % 2 == 0 ? "active" : ""));
                    i++;
                }
                result.Message = listBoxHTML.ToString();
            }
            return Content(ConvertTools._ConvertTools.SerializeObject(result));
        }

        public ActionResult PerformanceRanking()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PerformanceRankingGet(ParamUserList param)
        {
            var result = new WxService().GetUserRankingList(SessionHelper._SessionHelper.UserID, param);
            if (result.Ret > 0)
            {
                var listBoxHTML = new StringBuilder();
                if (result.Data != null && !result.Data.Any())
                    listBoxHTML.Append("<div style='display: none;'>----已经到底了----</div>");
                else
                {
                    int i = 1;
                    foreach (var user in result.Data)
                    {
                        listBoxHTML.Append(string.Format(@"<div class='mui-row tr tr{4}'>
	                        <div class='mui-pull-left cz cz1'>{0}</div>
	                        <div class='mui-pull-left cz cz2'>{1}</div>
	                        <div class='mui-pull-left cz cz3'>{2}</div>
	                        <div class='mui-pull-left cz cz4'>{3}</div></div>",
                            (param.PageIndex - 1) * param.PageSize + i, user.UserId, user.NickName, user.ChargeAmount, i % 2 == 0 ? "2" : "1"));
                        i++;
                    }
                }
                result.Message = listBoxHTML.ToString();
            }
            return Content(ConvertTools._ConvertTools.SerializeObject(result));
        }

        public ActionResult UserGame()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserGameGet(ParamUserList param)
        {
            var result = new WxService().UserGameGet(SessionHelper._SessionHelper.UserID, param);
            if (result.Ret > 0)
            {
                var listBoxHTML = new StringBuilder();
                if (result.Data != null && !result.Data.Any())
                    listBoxHTML.Append("<div style='display: none;'>----已经到底了----</div>");
                else
                {
                    decimal? round;
                    decimal? roundPercent;
                    int i = 1;
                    foreach (var user in result.Data)
                    {
                        if (param.Type == 1)
                        {
                            round = user.TodayRound;
                            roundPercent = user.TodayRound == null || user.TodayRound == 0 ? 0 :
                                Convert.ToDecimal(Math.Round(user.TodayWinRound ?? 0 / user.TodayRound ?? 0, 2));
                        }
                        else
                        {
                            round = user.TotalRound;
                            roundPercent = user.TotalRound == null || user.TotalRound == 0 ? 0 : 
                                Convert.ToDecimal(Math.Round(user.TotalWinRound ?? 0 / user.TotalRound ?? 0, 2));
                        }

                        listBoxHTML.Append(string.Format(@"<div class='mui-row tr tr{5}'>
	                        <div class='mui-pull-left cz cz2'>{0}</div>
	                        <div class='mui-pull-left cz cz3'>{1}</div>
	                        <div class='mui-pull-left cz cz4'>{2}</div>
	                        <div class='mui-pull-left cz cz1'>{3}</div>
	                        <div class='mui-pull-left cz cz5'>{4}</div></div>",
                            user.UserId, user.NickName, round ?? 0, roundPercent ?? 0, user.Diamond ?? 0, i % 2 == 0 ? "2" : "1"));
                        i++;
                    }
                }
                result.Message = listBoxHTML.ToString();
            }
            return Content(ConvertTools._ConvertTools.SerializeObject(result));
        }

        public ActionResult MyCenter()
        {
            var user = new WxService().GetUserById(SessionHelper._SessionHelper.UserID);
            var agent = new WxService().GetAgentById(SessionHelper._SessionHelper.UserID);
            var mua = new DtoRebateStatistics { user = user, agent = agent };
            if (user != null)
                mua.CanRebateAmount = new WxService().GetCanRebateAmount(user.GameId, user.UserId);

            return View(mua);
        }
    }
}