using System;
using System.Web.Mvc;
using System.Web.Security;
using Common.Tools;
using Creeper.WX.Utils;
using Entity.Base;
using Service;
using System.Text;

namespace Creeper.WX.Controllers
{
    [UserAuthorFilter(false)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var appid = Common.WxPay.Lib.PayConfig.WxAppid();
            var ts = Common.WxShare.Lib.ShareUtil.getTimestamp();
            var ns = Common.WxShare.Lib.ShareUtil.getNoncestr();
            var sign = Common.WxShare.JsApiShare.GetShareSignature(ns, ts);
            ViewBag.appid = appid;
            ViewBag.ts = ts;
            ViewBag.ns = ns;
            ViewBag.sign = sign;

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgentInfoLogin(int GameID, string Mobile, string Code)
        {
            ResultModel<User> result;
            if (string.IsNullOrEmpty(Code))
                result = "验证码不能为空".SetResult<User>(null);
            else if(string.IsNullOrEmpty(Mobile))
                result = "手机号不能为空".SetResult<User>(null);
            else
                result = new WxService().AgentInfoLogin(GameID, Mobile);

            if (result.Ret > 0)
            {
                SessionHelper._SessionHelper.UserID = Convert.ToString(result.Data.Id);
                SessionHelper._SessionHelper.UserName = result.Data.NickName;
            }

            return Content(ConvertTools._ConvertTools.SerializeObject(result));
        }

        public ActionResult OAuth(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                var code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                    returnUrl += (returnUrl.Contains("?") ? "&" : "?") + "code=" + code;

                return Redirect(returnUrl);
            }
            return Redirect("Error");
        }

        public ActionResult Error()
        {
            return View();
        }

        #region 微信公众号——服务器配置
        public ActionResult Server(string signature, string timestamp, string nonce, string echostr)
        {
            try
            {
                string echoStr = Valid(signature, timestamp, nonce, echostr);

                if (!string.IsNullOrEmpty(echoStr))
                    return Content(echostr, "application/x-www-form-urlencoded", Encoding.UTF8);

                return Content("验证失败!", "application/x-www-form-urlencoded", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString(), "application/x-www-form-urlencoded", Encoding.UTF8);
            }
        }

        private string Valid(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature(signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echostr))
                {
                    return echostr;
                }
            }

            return "";
        }

        private readonly string Token = "JIAZHAOKAI";//与微信公众账号后台的Token设置保持一致，区分大小写。

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature(string signature, string timestamp, string nonce)
        {
            string[] arrTmp = { Token, timestamp, nonce };
            Array.Sort(arrTmp); //字典排序
            string tmpStr = string.Join("", arrTmp);
#pragma warning disable 618
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
#pragma warning restore 618
            if (tmpStr != null)
            {
                tmpStr = tmpStr.ToLower();
                if (tmpStr == signature)
                    return true;
            }
            return false;
        }
        #endregion
    }
}