using Common.Tools;
using System;
using System.Net;

namespace Common
{
    public class DataCaptureService
    {
        private string strFileName = "___Creeper";
        private int _pageSize = 100;
        private string _sortIndex = "";

        /// <summary>
        /// 验证码
        /// </summary>
        /// <param name="mc"></param>
        public string GetValidateImage(ref MyClass mc)
        {
            string domain = "http://platform.xy.qianz.com/DataCenter/VisitorData.aspx?";
            var url = string.Format(@"{0}method=GetCode&time=0.3439712790614875", domain);
            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "image/webp,image/apng,image/*,*/*",
                ContentType = "application/x-www-form-urlencoded",
                Referer = "http://platform.xy.qianz.com/Pages/login.html",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };
            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            return data;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="code"></param>
        public string Login(ref MyClass mc, string code = "")
        {
            var postdata = "username=jiazhiyi&password=789453f7d1f78abe3711e80dba307728&code=" + code;
            var url = "http://platform.xy.qianz.com/DataCenter/VisitorData.aspx?method=Login";
            mc = new MyClass
            {
                Url = url,
                Method = "POST",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                Referer = "http://platform.xy.qianz.com/Pages/login.html",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = postdata,
            };
            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            LogHelper.WriteToLog("Login[登录]:" + data, strFileName);
            return data;
        }


        /// <summary>
        /// 所有产品
        /// </summary>
        /// <param name="mc"></param>
        public string GetAllProduct(ref MyClass mc)
        {
            _sortIndex = "GameId";
            string domain = "http://platform.xy.qianz.com/DataCenter/SysData.aspx?method=GetAllProduct";
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                Referer = "http://platform.xy.qianz.com/Pages/ProductPages/ProductsManage.html",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer??new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("GetAllProduct[所有产品]:" + data, strFileName);
            return data;
        }

        /// <summary>
        /// 所有产品 代理层级
        /// </summary>
        /// <param name="mc"></param>
        public string GetAllAgentLevel(ref MyClass mc)
        {
            _sortIndex = "GameId";
            string domain = "http://platform.xy.qianz.com/DataCenter/SysData.aspx?method=GetAllAgentLevel";
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                Referer = "http://platform.xy.qianz.com/Pages/AgencyPages/ProductAgencyLevel.html",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("GetAllAgentLevel[所有产品 代理层级]:" + data, strFileName);
            return data;
        }


        /// <summary>
        /// 代理管理
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="gid"></param>
        public string GetAllAgent(ref MyClass mc, int gid = 0)
        {
            _pageSize = 2000;
            _sortIndex = "CreateTime";
            string domain = "http://platform.xy.qianz.com/DataCenter/SysData.aspx?method=GetAllAgent";
            domain += string.Format("&gid={0}&guid=&agentstatus=-1", gid);
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("GetAllAgent[代理管理]:" + data, strFileName);
            return data;
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="gid"></param>
        public string GetAllUser(ref MyClass mc, int gid = 0)
        {
            _pageSize = 50000;
            _sortIndex = "UserId";
            string domain = "http://platform.xy.qianz.com/DataCenter/StatisticalData.aspx?method=EachProductUserDistributionDetail";
            domain += string.Format("&gid={0}&gameuid=", gid);
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                Referer = "http://platform.xy.qianz.com/Pages/GamePages/GameUserManage.html",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //if (data.Length < 2000)
            //    LogHelper.WriteToLog("GetAllUser[用户管理]:" + data, strFileName);
            return data;
        }

        /// <summary>
        /// 即时返利明细
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="gid"></param>
        /// <param name="sTime"></param>
        /// <param name="eTime"></param>
        public string InstantRebatesAnalysis(ref MyClass mc, int gid = 0, string sTime = "", string eTime = "")
        {
            _pageSize = 10000;
            _sortIndex = "Writedate";
            string domain = "http://platform.xy.qianz.com/DataCenter/StatisticalData.aspx?method=InstantRebatesAnalysis";
            domain += string.Format("&gid={0}&guid=&uid=&status=1&begintime={1}&endtime={2}", gid, sTime, eTime);
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("InstantRebatesAnalysis[即时返利明细]:" + data, strFileName);
            return data;
        }


        public string InstantChargeAnalysis(ref MyClass mc, int gid = 0, string sTime = "", string eTime = "")
        {
            _pageSize = 10000;
            _sortIndex = "ChargeTime";
            string domain = "http://platform.xy.qianz.com/DataCenter/StatisticalData.aspx?method=EachProductFlowStatisticsDetail";
            domain += string.Format("&gid={0}&guser=&uid=&status=1&begintime={1}&endtime={2}", gid, sTime, eTime);
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("InstantRebatesAnalysis[充值流水一览]:" + data, strFileName);
            return data;
        }


        /// <summary>
        /// 代理商申请
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="gid"></param>
        public string GetAllAgentApply(ref MyClass mc, int gid = 0)
        {
            _pageSize = 5000;
            _sortIndex = "ApplyTime";
            string domain = "http://platform.xy.qianz.com/DataCenter/SysData.aspx?method=GetAgentApply&passflag=-1";
            domain += string.Format("&gid={0}&guid=", gid);
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("GetAgentApply[代理商申请]:" + data, strFileName);
            return data;
        }



        /// <summary>
        /// 代理一览 详情
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="gid"></param>
        /// <param name="aglid"></param>
        public string EachProductAgentCountDistributionDetail(ref MyClass mc, int gid = 10013, int aglid = 1)
        {
            _pageSize = 500;
            _sortIndex = "UserId";
            string domain = "http://platform.xy.qianz.com/DataCenter/StatisticalData.aspx?method=EachProductAgentCountDistributionDetail";
            domain += string.Format("&gid={0}&aglid={1}&key=&puid=0&begintime=&endtime=", gid, aglid);
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("EachProductAgentCountDistributionDetail[代理一览 详情]:" + data, strFileName);
            return data;
        }
        /// <summary>
        /// 用户一览 详情
        /// </summary>
        public string EachProductUserDistributionDetail(ref MyClass mc, int gid = 10013)
        {
            _pageSize = 500;
            _sortIndex = "RegisterTime";
            string domain = "http://platform.xy.qianz.com/DataCenter/StatisticalData.aspx?method=EachProductUserDistributionDetail";
            domain += string.Format("&gid={0}&agentuid=0", gid);
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("EachProductUserDistributionDetail[用户一览详情]:" + data, strFileName);
            return data;
        }
        /// <summary>
        /// 日结返利明细
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="gid"></param>
        public string DayRebatesAnalysis(ref MyClass mc, int gid = 0)
        {
            _pageSize = 5000;
            _sortIndex = "Writedate";
            string domain = "http://platform.xy.qianz.com/DataCenter/StatisticalData.aspx?method=DayRebatesAnalysis";
            domain += string.Format("&gid={0}&uid=&status=1&begintime=&endtime=", gid);
            var url = string.Format(@"{0}&_search=false&nd={1}&rows={2}&page=1&sidx={3}&sord=asc", domain, DateTime.Now.ToString("yyyyMMddHHmmssfff"), _pageSize, _sortIndex);

            mc = new MyClass
            {
                Url = url,
                Method = "GET",
                Accept = "application/json, text/javascript, */*",
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true,
                CookieContainer = mc.CookieContainer ?? new CookieContainer(),
                Headers = new WebHeaderCollection
                {
                    {"Accept-Language", "zh-cn,zh"},
                    {"Accept-Encoding", "gzip,deflate"},
                },
                Postdata = "",
            };

            var data = ToolsHelper._HttpHelper.SendAsyncHttp(ref mc);
            //LogHelper.WriteToLog("DayRebatesAnalysis[日结返利明细]:" + data, strFileName);
            return data;
        }
    }
}
