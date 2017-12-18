using Common;
using Common.WxPay.Lib;
using System;
using System.Web;
using System.Web.Mvc;
using Common.Tools;
using Common.WxModel;
using Entity.Base;
using Service;

namespace Creeper.WX.Utils
{
    public class UserAuthorFilterAttribute : ActionFilterAttribute
    {
        private static readonly string exLogFile = "___Exception_WxBissness";
        private readonly bool _isAuthorize;

        public UserAuthorFilterAttribute(bool isAuthorize = true)
        {
            this._isAuthorize = isAuthorize;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_isAuthorize)
            {
                string returnUrl = filterContext.HttpContext.Request.Url != null ? filterContext.HttpContext.Request.Url.AbsolutePath : "";
                //SessionHelper._SessionHelper.UserID = "10032|10028501";
                if (string.IsNullOrEmpty(SessionHelper._SessionHelper.UserID))
                {
                    string userAgent = filterContext.HttpContext.Request.UserAgent;
                    if (userAgent != null && userAgent.ToLower().Contains("micromessenger"))
                    {
                        // 首页不过滤
                        //if ("/" == returnUrl || string.IsNullOrEmpty(returnUrl))
                        //{
                        //    base.OnActionExecuting(filterContext);
                        //    return;
                        //}

                        string redirectUrl = GetReturnPath(filterContext, returnUrl);
                        if (!string.IsNullOrEmpty(redirectUrl))
                        {
                            redirectUrl += (redirectUrl.Contains("?") ? "&" : "?") + "ReturnUrl=" + returnUrl;
                            filterContext.HttpContext.Response.Redirect(redirectUrl);
                            filterContext.HttpContext.Response.End();
                            return;
                        }
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Redirect("/Home/Login");
                        filterContext.HttpContext.Response.End();
                        return;
                    }
                }
                base.OnActionExecuting(filterContext);
            }
        }

        private static string GetReturnPath(ActionExecutingContext filterContext, string returnUrl)
        {
            try
            {
                // Session.UserID 为空，则拉取用户注册信息
                if (string.IsNullOrEmpty(SessionHelper._SessionHelper.UserID))
                {
                    var code = filterContext.HttpContext.Request.QueryString["code"];
                    if (string.IsNullOrEmpty(code))
                    {
                        // CODE 为空，则根据appid拉取网页授权
                        var redirectUri = HttpUtility.UrlEncode(PayConfig.WebSiteDomain() + "/Home/OAuth?returnUrl=" + returnUrl);
                        string url = string.Format(@"https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect",
                            PayConfig.WxAppid(), redirectUri);
                        filterContext.HttpContext.Response.Redirect(url);
                        filterContext.HttpContext.Response.End();
                        return string.Empty;
                    }

                    // CODE 不为空，则根据 code获取用户信息，并注册
                    Registered(code);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog("[微信拉取网页授权信息异常]:" + ex, exLogFile);
            }
            return string.Empty;
        }

        private static User Registered(string code)
        {
            try
            {
                var client = new System.Net.WebClient { Encoding = System.Text.Encoding.UTF8 };

                // 通过code换取网页授权access_token 
                var strTokenUrl = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                    PayConfig.WxAppid(), PayConfig.WxAppSecret(), code);
                var strTokenData = client.DownloadString(strTokenUrl);
                var tokenEntity = ConvertTools._ConvertTools.DeserializeObject<WxAccessToken>(strTokenData);
                if (tokenEntity == null || string.IsNullOrEmpty(tokenEntity.access_token))
                    return null;


                // 通过access_token和openid 拉取用户信息
                var strUserUrl = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", tokenEntity.access_token, tokenEntity.openid);
                var strUserData = client.DownloadString(strUserUrl);
                var userEntity = ConvertTools._ConvertTools.DeserializeObject<WxUserInfo>(strUserData);
                if (userEntity == null || string.IsNullOrEmpty(userEntity.openid))
                    return null;


                // 注册
                var user = new WxService().Regist(userEntity);
                if (user != null)
                {
                    SessionHelper._SessionHelper.UserID = Convert.ToString(user.Id);
                    SessionHelper._SessionHelper.UserName = user.NickName;
                    return user;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog("[微信拉取网页授权信息异常]:" + ex, exLogFile);
            }
            return null;
        }
    }
}