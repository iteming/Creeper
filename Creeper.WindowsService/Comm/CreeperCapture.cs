using Common;
using Common.Tools;
using Entity;
using Entity.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;

namespace Creeper.WindowsService.Comm
{
    public class CreeperCapture
    {
        private static string _exLogFile = "___Exception";
        private static string _strFileName = "___Creeper";
        static MyClass _mc;
        static DataCaptureService _cs;
        static DbHelper _db;

        /// <summary>
        /// 执行数据抓取
        /// </summary>
        public static void DoCapture()
        {
            _mc = new MyClass();
            _cs = new DataCaptureService();
            _db = new DbHelper();

            LogHelper.WriteToLog("执行数据抓取 -- 开始：", _strFileName);

            LogHelper.LogFuncRuntime(_strFileName, "登录", Login);
            LogHelper.LogFuncRuntime(_strFileName, "获取产品", GetAllProduct);
            LogHelper.LogFuncRuntime(_strFileName, "获取产品等级", GetAllAgentLevel);
            LogHelper.LogFuncRuntime(_strFileName, "获取代理申请", GetAllAgentApply);
            LogHelper.LogFuncRuntime(_strFileName, "获取代理", GetAllAgent);
            LogHelper.LogFuncRuntime(_strFileName, "获取用户", GetAllUser);
            LogHelper.LogFuncRuntime(_strFileName, "获取充值记录", GetCharge);
            LogHelper.LogFuncRuntime(_strFileName, "推广即时返利记录", PromoterRebate);

            LogHelper.WriteToLog("执行数据抓取 -- 结束：", _strFileName);

            //LogHelper.LogFuncRuntime(strFileName, "获取返利记录", GetRebate);
            //_cs.GetValidateImage(ref mc);
            //_cs.EachProductAgentCountDistributionDetail(ref mc);
            //_cs.EachProductUserDistributionDetail(ref mc);
            //_cs.DayRebatesAnalysis(ref mc);
        }

        public static void NextDayRebate()
        {
            _db = new DbHelper();
            LogHelper.WriteToLog("隔日返利 -- 开始：", _strFileName);
            LogHelper.LogFuncRuntime(_strFileName, "推广隔日返利记录", PromoterNextDayRebate);
            LogHelper.WriteToLog("隔日返利 -- 结束：", _strFileName);
        }
        
        /// <summary>
        /// 登录 获取 ASP.NET_SessionID
        /// </summary>
        private static string Login()
        {
            try
            {
                var resultStr = _cs.Login(ref _mc);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModel<List<Admin>>>(resultStr);
                // 登录成功
                if (resultEntity.Ret == 1 && resultEntity.Data != null)
                {
                    var entity = resultEntity.Data.FirstOrDefault();
                    var rep = new Repository<Admin>(_db);
                    var dbEntity = rep.Get(a => a.AccountId == entity.AccountId).FirstOrDefault();
                    if (string.IsNullOrEmpty(entity.Password))
                        entity.Password = "789453f7d1f78abe3711e80dba307728";

                    if (dbEntity == null)
                        rep.Insert(entity);
                    else
                    {
                        if (string.IsNullOrEmpty(dbEntity.Password))
                            dbEntity.Password = entity.Password;

                        dbEntity.AccountId = entity.AccountId;
                        dbEntity.IsPrimary = entity.IsPrimary;
                        dbEntity.RoleId = entity.RoleId;
                        dbEntity.RoleName = entity.RoleName;
                        dbEntity.UserName = entity.UserName;

                        rep.Update(dbEntity);
                    }
                }

                return resultStr;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[用户登录异常]:" + e, _exLogFile);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取所有产品
        /// </summary>
        private static string GetAllProduct()
        {
            try
            {
                var resultStr = _cs.GetAllProduct(ref _mc);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<Product>>>(resultStr);
                var listInsert = new List<Product>();
                var listUpdate = new List<Product>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    var rep = new Repository<Product>(_db);
                    foreach (var entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(a => a.GameId == entity.GameId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(entity);
                        else
                        {
                            dbEntity.GameId = entity.GameId;
                            dbEntity.GameName = entity.GameName;
                            dbEntity.IsValid = entity.IsValid;
                            dbEntity.Remark = entity.Remark;
                            dbEntity.ServerApiUrl = entity.ServerApiUrl;
                            dbEntity.DBWanIP = entity.DBWanIP;
                            dbEntity.DBIntranetIP = entity.DBIntranetIP;
                            dbEntity.DBProt = entity.DBProt;

                            listUpdate.Add(dbEntity);
                        }
                        #endregion
                    }

                    // 批量新增
                    if (listInsert.Count > 0)
                        rep.Insert(listInsert);

                    // 批量修改
                    if (listUpdate.Count > 0)
                        rep.Update(listUpdate);
                }
                return resultStr;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取产品异常]:" + e, _exLogFile);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取所有产品分利信息
        /// </summary>
        private static string GetAllAgentLevel()
        {
            try
            {
                var resultStr = _cs.GetAllAgentLevel(ref _mc);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<AgentLevel>>>(resultStr);
                var listInsert = new List<AgentLevel>();
                var listUpdate = new List<AgentLevel>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    var rep = new Repository<AgentLevel>(_db);
                    foreach (var entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(a => a.GameId == entity.GameId && a.AgentLevelId == entity.AgentLevelId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(entity);
                        //else
                        //{
                        //    dbEntity.GameId = Entity.GameId;
                        //    dbEntity.GameName = Entity.GameName;
                        //    dbEntity.IsValid = Entity.IsValid;
                        //    dbEntity.AgentLevelId = Entity.AgentLevelId;
                        //    dbEntity.AgentLevelName = Entity.AgentLevelName;
                        //    dbEntity.DProportion = Entity.DProportion;
                        //    dbEntity.IProportion = Entity.IProportion;
                        //    dbEntity.IProportion2 = Entity.IProportion2;
                        //    listUpdate.Add(dbEntity);
                        //}
                        #endregion
                    }

                    listInsert.ForEach(a => a.Id = a.GameId + "|" + a.AgentLevelId);
                    listUpdate.ForEach(a => a.Id = a.GameId + "|" + a.AgentLevelId);

                    // 批量新增
                    if (listInsert.Count > 0)
                        rep.Insert(listInsert);

                    // 批量修改
                    if (listUpdate.Count > 0)
                        rep.Update(listUpdate);
                }
                return resultStr;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取产品代理层级异常]:" + e, _exLogFile);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取代理商申请
        /// </summary>
        private static string GetAllAgentApply()
        {
            try
            {
                var confGameId = Convert.ToInt32(ConfigurationManager.AppSettings["GameID"]);
                var resultStr = _cs.GetAllAgentApply(ref _mc, confGameId);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<AgentApply>>>(resultStr);
                var listInsert = new List<AgentApply>();
                var listUpdate = new List<AgentApply>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    var rep = new Repository<AgentApply>(_db);
                    foreach (var entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(a => a.GameId == entity.GameId && a.UserId == entity.UserId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(entity);
                        else
                        {
                            // 本平台构造的代理申请
                            //if (dbEntity.Platform == 1)
                            //    continue;

                            dbEntity.GameId = entity.GameId;
                            dbEntity.GameName = entity.GameName;

                            dbEntity.UserId = entity.UserId;
                            dbEntity.RealName = entity.RealName;
                            dbEntity.NickName = entity.NickName;
                            dbEntity.PhoneNo = entity.PhoneNo;

                            dbEntity.RegisterTime = entity.RegisterTime;
                            dbEntity.GameRounds = entity.GameRounds;
                            dbEntity.RoomCardUsed = entity.RoomCardUsed;

                            dbEntity.ApplyTime = entity.ApplyTime;
                            dbEntity.AuditTime = entity.AuditTime;
                            dbEntity.PassFlag = entity.PassFlag;
                            dbEntity.Remark = entity.Remark;

                            listUpdate.Add(dbEntity);
                        }
                        #endregion
                    }

                    // 批量新增
                    if (listInsert.Count > 0)
                        rep.Insert(listInsert);

                    // 批量修改
                    if (listUpdate.Count > 0)
                        rep.Update(listUpdate);
                }
                return resultStr;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取代理申请]:" + e, _exLogFile);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取所有代理
        /// </summary>
        private static string GetAllAgent()
        {
            try
            {
                var confGameId = Convert.ToInt32(ConfigurationManager.AppSettings["GameID"]);
                var resultStr = _cs.GetAllAgent(ref _mc, confGameId);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<Agent>>>(resultStr);
                var listInsert = new List<Agent>();
                var listUpdate = new List<Agent>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    var rep = new Repository<Agent>(_db);
                    foreach (var entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(a => a.GameId == entity.GameId && a.UserId == entity.UserId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(entity);
                        else
                        {
                            // 对方平台审核通过，成为代理后。是否替换本系统审核通过成为推广员的信息
                            if (dbEntity.Platform == 1 && !Convert.ToBoolean(ConfigurationManager.AppSettings["ReplaceAgent"]))
                                continue;

                            dbEntity.GameId = entity.GameId;
                            dbEntity.GameName = entity.GameName;
                            dbEntity.UserId = entity.UserId;
                            dbEntity.RealName = entity.RealName;
                            dbEntity.NickName = entity.NickName;
                            dbEntity.PhoneNo = entity.PhoneNo;
                            dbEntity.MyAgentLevel = entity.MyAgentLevel;
                            dbEntity.AgentLevelName = entity.AgentLevelName;

                            dbEntity.ParentUserId = entity.ParentUserId;
                            dbEntity.AgentLevel1 = entity.AgentLevel1;
                            dbEntity.AgentLevel2 = entity.AgentLevel2;
                            //dbEntity.AgentLevel3 = Entity.AgentLevel3;

                            dbEntity.AgentCode = entity.AgentCode;
                            dbEntity.AgentStatus = entity.AgentStatus;
                            dbEntity.CreateTime = entity.CreateTime;

                            //dbEntity.DProportion = Entity.DProportion;
                            //dbEntity.IProportion = Entity.IProportion;
                            //dbEntity.IProportion2 = Entity.IProportion2;

                            listUpdate.Add(dbEntity);
                        }
                        #endregion
                    }

                    listInsert.ForEach(a => a.Id = a.GameId + "|" + a.UserId);
                    listUpdate.ForEach(a => a.Id = a.GameId + "|" + a.UserId);

                    // 批量新增
                    if (listInsert.Count > 0)
                        rep.Insert(listInsert);

                    // 批量修改
                    if (listUpdate.Count > 0)
                        rep.Update(listUpdate);
                }
                return resultStr;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取代理异常]:" + e, _exLogFile);
                return e.Message;
            }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        private static string GetAllUser()
        {
            try
            {
                var confGameId = Convert.ToInt32(ConfigurationManager.AppSettings["GameID"]);
                var resultStr = _cs.GetAllUser(ref _mc, confGameId);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<User>>>(resultStr);
                var listInsert = new List<User>();
                var listUpdate = new List<User>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    //// 根据规则获取所有推广员
                    //var listPromoter = GetPromoterOld(resultEntity.rows);
                    resultEntity.rows = GetPromoter(resultEntity.rows);

                    var rep = new Repository<User>(_db);
                    foreach (var entity in resultEntity.rows)
                    {
                        //// 循环的本次用户 Entity 是否是推广员
                        //if (listPromoter.Count(a => a.GameId == Entity.GameId && a.UserId == Entity.UserId) > 0)
                        //{
                        //    Entity.MyAgentLevel = ConstClass.PromoterLevelId;
                        //    Entity.UserType = ConstClass.PromoterLevelName;
                        //}

                        #region 构造新增和修改的实体List
                        var dbEntity = rep.Get(a => a.GameId == entity.GameId && a.UserId == entity.UserId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(entity);
                        else
                        {
                            //// 本平台构造的用户不更新
                            //if (dbEntity.Platform == 1)
                            //    continue;
                            
                            dbEntity.GameId = entity.GameId;
                            dbEntity.GameName = entity.GameName;
                            dbEntity.UserId = entity.UserId;
                            dbEntity.NickName = entity.NickName;
                            dbEntity.PhoneNo = entity.PhoneNo;
                            dbEntity.MyAgentLevel = entity.MyAgentLevel;
                            dbEntity.UserType = entity.UserType;
                            dbEntity.RegisterTime = entity.RegisterTime;
                            dbEntity.LastLoginTime = entity.LastLoginTime;

                            dbEntity.ParentUserId = entity.ParentUserId;
                            dbEntity.AgentRealName = entity.AgentRealName;
                            dbEntity.BindingTime = entity.BindingTime;

                            dbEntity.FriendID = entity.FriendID;
                            dbEntity.FriendNickName = entity.FriendNickName;
                            dbEntity.FBindingTime = entity.FBindingTime;


                            dbEntity.AgentLevel1 = entity.AgentLevel1;
                            dbEntity.AgentLevel2 = entity.AgentLevel2;
                            dbEntity.AgentLevel3 = entity.AgentLevel3;

                            dbEntity.TotalCharge = entity.TotalCharge;
                            dbEntity.TodayCharge = entity.TodayCharge;
                            dbEntity.TotalRound = entity.TotalRound;
                            dbEntity.TodayRound = entity.TodayRound;
                            dbEntity.TotalWinRound = entity.TotalWinRound;
                            dbEntity.TodayWinRound = entity.TodayWinRound;
                            dbEntity.Diamond = entity.Diamond;

                            dbEntity.OpenId = entity.OpenId;
                            dbEntity.UnionId = entity.UnionId;
                            
                            //dbEntity.Coin = Entity.Coin;
                            //dbEntity.GameVersion = Entity.GameVersion;

                            listUpdate.Add(dbEntity);
                        }
                        #endregion
                    }

                    listInsert.ForEach(a => a.Id = a.GameId + "|" + a.UserId);
                    listUpdate.ForEach(a => a.Id = a.GameId + "|" + a.UserId);

                    // 批量新增
                    if (listInsert.Count > 0)
                        rep.Insert(listInsert);

                    // 批量修改
                    if (listUpdate.Count > 0)
                        rep.Update(listUpdate);
                }
                return resultStr;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取用户异常]:" + e, _exLogFile);
                return e.Message;
            }
        }

        /// <summary>
        /// 筛选符合规则的推广员(理解错误，暂时作废)
        /// </summary>
        /// <param name="list">抓取到的所有用户数据</param>
        /// <returns></returns>
        private static List<User> GetPromoterOld(List<User> list)
        {
            /** 推广员逻辑
             * 用户a和用户B的用户级别 MyAgentLevel = 0 (普通用户)
             * 如果用户B的 FriendID (好友ID) = 用户a.UserId 
             * 则用户a 属于推广员
            **/

            var listNew =
                (from a in list
                 join b in list
                 on new { GameId = a.GameId, UserId = a.UserId } equals new { GameId = b.GameId, UserId = b.FriendID ?? 0 }
                 where a.MyAgentLevel == 0 && b.MyAgentLevel == 0
                 group a by new { a.GameId, a.UserId } into g
                 select new User { GameId = g.Key.GameId, UserId = g.Key.UserId }).ToList();
            return listNew;
        }

        /// <summary>
        /// 万能的根据 AgentLevel信息 查找符合条件的下级推广员
        /// 推广员之间的关联关系在此建立
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        private static List<User> GetPromoter(List<User> userList)
        {
            var rep = new Repository<AgentLevel>(_db);
            var levelList = rep.Get().Where(a => a.AgentLevelId > 2).OrderBy(a => a.GameId).ThenBy(a => a.AgentLevelId);

            foreach (var level in levelList)
            {
                // 本次应该查找的用户等级
                var thisLevelId = level.AgentLevelId != 3 ? level.AgentLevelId - 1 : level.AgentLevelId;

                // 查找 父级为本次查找等级 的用户id
                var userArray =
                    (from b in userList
                     join a in userList
                        on new { GameId = b.GameId, UserId = b.ParentUserId ?? 0 } equals new { GameId = a.GameId, UserId = a.UserId } 
                     where a.GameId == level.GameId && a.MyAgentLevel == thisLevelId && b.MyAgentLevel == thisLevelId
                    group b by b.UserId into g
                    select new { userid = g.Key }).Select(a => a.userid).ToArray();

                // 根据用户id查找具体的用户
                var thisUserList = userList.Where(a => a.GameId == level.GameId && userArray.Contains(a.UserId)).ToList();

                // 修改这些用户的代理/推广等级
                foreach (var user in thisUserList)
                {
                    user.MyAgentLevel = level.AgentLevelId;
                    user.UserType = level.AgentLevelName;
                }
            }
            return userList;
        }

        /// <summary>
        /// 获取所有充值订单
        /// </summary>
        private static string GetCharge()
        {
            try
            {
                var rep = new Repository<Charge>(_db);
                int runInterval = Convert.ToInt32(ConfigurationManager.AppSettings["RunInterval"]);
                var sTime = ConfigurationManager.AppSettings["BeginTime"];
                var eTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59");

                #region 历史订单只获取一次
                // 获取非本平台在指定时间段提交的订单
                var tempSTime = Convert.ToDateTime(sTime);
                var tempETime = Convert.ToDateTime(eTime);
                var historyCount = rep.Get(a => a.ChargeTime >= tempSTime && a.ChargeTime <= tempETime).Count();
                if (historyCount > 0)
                {
                    sTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                    eTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    eTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                #endregion

                #region 历史今日订单只获取一次
                // 获取非本平台在指定时间段提交的订单
                var temp2STime = Convert.ToDateTime(sTime);
                var temp2ETime = Convert.ToDateTime(eTime);
                var history2Count = rep.Get(a => a.ChargeTime >= temp2STime && a.ChargeTime <= temp2ETime).Count();
                if (history2Count > 0)
                {
                    sTime = DateTime.Now.AddMilliseconds(-1 * runInterval).ToString("yyyy-MM-dd HH:mm:ss");
                    eTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                #endregion

                var confGameId = Convert.ToInt32(ConfigurationManager.AppSettings["GameID"]);

                var resultStr = _cs.InstantChargeAnalysis(ref _mc, confGameId, sTime, eTime);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<Charge>>>(resultStr);
                var listInsert = new List<Charge>();
                var listUpdate = new List<Charge>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    foreach (var entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(a => a.OrderNo == entity.OrderNo).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(entity);
                        else
                        {
                            dbEntity.GameId = entity.GameId;
                            dbEntity.GameName = entity.GameName;

                            dbEntity.UserId = entity.UserId;
                            dbEntity.NickName = entity.NickName;

                            dbEntity.OrderNo = entity.OrderNo;
                            dbEntity.Amount = entity.Amount;
                            dbEntity.ChargeTime = entity.ChargeTime;

                            listUpdate.Add(dbEntity);
                        }
                        #endregion
                    }

                    // 批量新增
                    if (listInsert.Count > 0)
                        rep.Insert(listInsert);

                    // 批量修改
                    if (listUpdate.Count > 0)
                        rep.Update(listUpdate);
                }

                return resultStr;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取订单异常]:" + e, _exLogFile);
                return e.Message;
            }

        }

        /// <summary>
        /// 获取所有充值返利订单
        /// </summary>
        private static string GetRebate()
        {
            try
            {
                var rep = new Repository<Rebate>(_db);

                int runInterval = Convert.ToInt32(ConfigurationManager.AppSettings["RunInterval"]);
                var sTime = ConfigurationManager.AppSettings["BeginTime"];
                var eTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59");

                #region 历史订单只获取一次
                // 获取非本平台在指定时间段提交的订单
                var tempSTime = Convert.ToDateTime(sTime);
                var tempETime = Convert.ToDateTime(eTime);
                var historyCount = rep.Get(a => a.Writedate >= tempSTime && a.Writedate <= tempETime && a.Platform == 0).Count();
                if (historyCount > 0)
                {
                    sTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                    eTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    eTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                #endregion

                #region 历史今日订单只获取一次
                // 获取非本平台在指定时间段提交的订单
                var temp2STime = Convert.ToDateTime(sTime);
                var temp2ETime = Convert.ToDateTime(eTime);
                var history2Count = rep.Get(a => a.Writedate >= temp2STime && a.Writedate <= temp2ETime && a.Platform == 0).Count();
                if (history2Count > 0)
                {
                    sTime = DateTime.Now.AddMilliseconds(-1 * runInterval).ToString("yyyy-MM-dd HH:mm:ss");
                    eTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                #endregion


                var confGameId = Convert.ToInt32(ConfigurationManager.AppSettings["GameID"]);
                var resultStr = _cs.InstantRebatesAnalysis(ref _mc, confGameId, sTime, eTime);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<Rebate>>>(resultStr);
                var listInsert = new List<Rebate>();
                var listUpdate = new List<Rebate>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    foreach (var entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(a => a.SourceOrderId == entity.SourceOrderId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(entity);
                        else
                        {
                            dbEntity.GameId = entity.GameId;
                            dbEntity.GameName = entity.GameName;

                            dbEntity.SourceOrderId = entity.SourceOrderId;
                            dbEntity.OrderId = entity.OrderId;
                            dbEntity.PaymentId = entity.PaymentId;

                            dbEntity.OrderState = entity.OrderState;
                            dbEntity.IsReceive = entity.IsReceive;

                            dbEntity.ChargeUserId = entity.ChargeUserId;
                            dbEntity.NickName = entity.NickName;

                            dbEntity.UserId = entity.UserId;
                            dbEntity.RealName = entity.RealName;

                            dbEntity.RebateAmount = entity.RebateAmount;
                            dbEntity.ChargeAmount = entity.ChargeAmount;
                            dbEntity.DProportion = entity.DProportion;
                            dbEntity.Writedate = entity.Writedate;

                            listUpdate.Add(dbEntity);
                        }
                        #endregion
                    }

                    // 批量新增
                    if (listInsert.Count > 0)
                        rep.Insert(listInsert);

                    // 批量修改
                    if (listUpdate.Count > 0)
                        rep.Update(listUpdate);
                }
                return resultStr;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取返利异常]:" + e, _exLogFile);
                return e.Message;
            }
        }

        /// <summary>
        /// 即时返利
        /// 根据充值订单 
        /// 构造符合条件的推广员返利订单
        /// </summary>
        private static string PromoterRebate()
        {
            try
            {
                var rep = new Repository<Charge>(_db);
                var repRebate = new Repository<Rebate>(_db);
                var repUser = new Repository<User>(_db);
                var repALevel = new Repository<AgentLevel>(_db);
                var listInsert = new List<Rebate>();

                int runInterval = Convert.ToInt32(ConfigurationManager.AppSettings["RunInterval"]);
                var sTime = Convert.ToDateTime(ConfigurationManager.AppSettings["BeginTime"]);
                var eTime = Convert.ToDateTime(DateTime.Now.AddMilliseconds(-1 * runInterval).ToString("yyyy-MM-dd 23:59:59"));

                #region 历史订单只获取一次
                // 获取非本平台在指定时间段提交的订单
                var historyCount = repRebate.Get(a => a.Writedate >= sTime && a.Writedate <= eTime && a.Platform == 1).Count();
                if (historyCount > 0)
                {
                    sTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                    eTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    eTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                #endregion

                #region 历史今日订单只获取一次
                var history2Count = repRebate.Get(a => a.Writedate >= sTime && a.Writedate <= eTime && a.Platform == 1).Count();
                if (history2Count > 0)
                {
                    sTime = Convert.ToDateTime(DateTime.Now.AddMilliseconds(-1 * runInterval).ToString("yyyy-MM-dd HH:mm:ss"));
                    eTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                #endregion

                // 获取在指定时间段提交的订单
                var resultList = rep.Get(a => a.ChargeTime >= sTime && a.ChargeTime <= eTime).ToList();
                foreach (var charge in resultList)
                {
                    // 若原平台订单已经生成 推广员返利订单，则跳过
                    if (repRebate.Get(a => a.OrderNo == charge.OrderNo && a.Platform == 1).Any())
                        continue;

                    // 获取当前充值账户是否存在上级 推广员
                    var listPreviousPromoter = GetParentUserMethod(repUser, charge.GameId, charge.UserId, 0);

                    // 该充值用户 charge.UserId 有上级推广员时
                    if (listPreviousPromoter.Count == 1)
                    {
                        // 获得上级推广员
                        var promoter = listPreviousPromoter.FirstOrDefault();
                        
                        // 创建推广员返利记录
                        CreatePromoterRebateMethod(repALevel, listInsert, charge, promoter, 0);
                    }
                    else if (listPreviousPromoter.Count > 1)
                    {
                        LogHelper.WriteToLog("[推广员即时返利记录错误]: 充值用户的上级推广员出现了2个及以上", _exLogFile);
                    }
                }

                // 批量新增
                if (listInsert.Count > 0)
                    repRebate.Insert(listInsert);

                return string.Empty;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[推广员即时返利记录异常]:" + e, _exLogFile);
                return e.Message;
            }
        }


        private static string PromoterNextDayRebate()
        {
            try
            {
                var rep = new Repository<Charge>(_db);
                var repRebate = new Repository<Rebate>(_db);
                var repUser = new Repository<User>(_db);
                var repALevel = new Repository<AgentLevel>(_db);
                var listInsert = new List<Rebate>();

                var sTime = Convert.ToDateTime(ConfigurationManager.AppSettings["BeginTime"]);
                var eTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));

                #region 历史订单只获取一次
                // 获取非本平台在指定时间段提交的订单
                var historyCount = repRebate.Get(a => a.Writedate >= sTime && a.Writedate <= eTime && a.Platform == 1 && a.RebateType != 0).Count();
                if (historyCount > 0)
                {
                    sTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"));
                    eTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
                }
                #endregion

                // 获取在指定时间段提交的订单
                var resultList = rep.Get(a => a.ChargeTime >= sTime && a.ChargeTime <= eTime).ToList();
                foreach (var charge in resultList)
                {
                    // 若原平台订单已经生成 推广员返利订单，则跳过
                    if (repRebate.Get(a => a.OrderNo == charge.OrderNo && a.Platform == 1 && a.RebateType != 0).Any())
                        continue;

                    #region 获得直属推广员
                    // 获得直属推广员
                    var listPreviousPromoter = GetParentUserMethod(repUser, charge.GameId, charge.UserId, 0);
                    // 该充值用户 charge.UserId 有直属推广员时
                    if (listPreviousPromoter.Count != 1 || listPreviousPromoter.Count == 0)
                        continue;
                    
                    // 获得直属推广员
                    var promoter = listPreviousPromoter.FirstOrDefault();
                    #endregion


                    #region 获得上级推广员
                    // 获得上级推广员
                    listPreviousPromoter = GetParentUserMethod(repUser, promoter.GameId, promoter.UserId, 1);
                    // 该充值用户 charge.UserId 有上级推广员时
                    if (listPreviousPromoter.Count != 1 || listPreviousPromoter.Count == 0)
                        continue;

                    // 获得上级推广员
                    promoter = listPreviousPromoter.FirstOrDefault();
                    // 创建上级推广员返利记录
                    CreatePromoterRebateMethod(repALevel, listInsert, charge, promoter, 1);
                    #endregion


                    #region 获得上上级推广员
                    // 获得上上级推广员
                    listPreviousPromoter = GetParentUserMethod(repUser, promoter.GameId, promoter.UserId, 2);
                    // 该充值用户 charge.UserId 有上上级推广员时
                    if (listPreviousPromoter.Count != 1 || listPreviousPromoter.Count == 0)
                        continue;

                    // 获得上上级推广员
                    promoter = listPreviousPromoter.FirstOrDefault();
                    // 创建上上级推广员返利记录
                    CreatePromoterRebateMethod(repALevel, listInsert, charge, promoter, 2);
                    #endregion


                    #region 获得上上上级推广员
                    // 获得上上上级推广员
                    listPreviousPromoter = GetParentUserMethod(repUser, promoter.GameId, promoter.UserId, 3);
                    // 该充值用户 charge.UserId 有上上上级推广员时
                    if (listPreviousPromoter.Count != 1 || listPreviousPromoter.Count == 0)
                        continue;

                    // 获得上上上级推广员
                    promoter = listPreviousPromoter.FirstOrDefault();
                    // 创建上上上级推广员返利记录
                    CreatePromoterRebateMethod(repALevel, listInsert, charge, promoter, 3);
                    #endregion
                }

                // 批量新增
                if (listInsert.Count > 0)
                    repRebate.Insert(listInsert);

                return string.Empty;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[推广员隔日返利记录异常]:" + e, _exLogFile);
                return e.Message;
            }
        }

        /// <summary>
        /// 创建推广员返利记录
        /// </summary>
        /// <param name="repALevel"></param>
        /// <param name="listInsert"></param>
        /// <param name="charge"></param>
        /// <param name="promoter"></param>
        /// <param name="userType">用户类型：0 直属用户，1 下级，2 下下级，3 下下下级</param>
        private static void CreatePromoterRebateMethod(Repository<AgentLevel> repALevel,
            List<Rebate> listInsert, Charge charge, User promoter, int userType = 0)
        {
            // 获得推广员应得返利的比例
            var agentLevelEntity = repALevel.Get(a => a.GameId == promoter.GameId
                && a.AgentLevelId == promoter.MyAgentLevel).FirstOrDefault();

            // 如果该用户的返利比例信息未找到，则跳过
            if (agentLevelEntity == null)
            {
                string strInfo = "promoter:【" + ConvertTools._ConvertTools.SerializeObject(promoter) + "】";
                if (userType == 0)
                    LogHelper.WriteToLog("[推广员即时返利记录错误]: 用户的返利比例信息未找到；" + strInfo, _exLogFile);
                else
                    LogHelper.WriteToLog("[推广员隔日返利记录错误]: 用户的返利比例信息未找到；" + strInfo, _exLogFile);
                return;
            }

            // 返利的比例
            decimal dProportion;
            switch (userType)
            {
                case 1:
                    dProportion = agentLevelEntity.IProportion;
                    break;
                case 2:
                    dProportion = agentLevelEntity.IProportion2;
                    break;
                case 3:
                    dProportion = agentLevelEntity.IProportion3;
                    break;
                default:
                    dProportion = agentLevelEntity.DProportion;
                    break;
            }

            // 返利的金额
            var rebateAmount = charge.Amount * dProportion;
            // 创建返利的记录
            var waitRebateEntity = new Rebate
            {
                GameId = charge.GameId,
                GameName = charge.GameName,
                ChargeUserId = charge.UserId,
                NickName = charge.NickName,
                ChargeAmount = charge.Amount,
                RebateAmount = rebateAmount,
                DProportion = dProportion,
                SourceOrderId = DateTime.Now.Ticks.ToString(),
                OrderId = "",
                PaymentId = "",
                IsReceive = 0,
                OrderState = 0,
                UserId = promoter != null ? promoter.UserId : 0,
                RealName = promoter != null ? promoter.NickName : "",
                Writedate = DateTime.Now,
                OrderNo = charge.OrderNo,
                Platform = 1,
                RebateType = userType
            };
            listInsert.Add(waitRebateEntity);
        }

        private static List<User> GetParentUserMethod(Repository<User> repUser, int gameId, int userId, int userType)
        {
            int agentLevelAblove = ConstClass.PromoterParentLevelId;
            if (userType == 0)
                agentLevelAblove = ConstClass.PromoterLevelId;

            // 获取当前充值账户是否存在上级 推广员
            var listPreviousPromoter =
                (from a in repUser.Get()
                 join b in repUser.Get()
                 on new { GameId = a.GameId, UserId = a.UserId } equals new { GameId = b.GameId, UserId = b.ParentUserId ?? 0 }
                 where a.MyAgentLevel >= agentLevelAblove && // 不筛选 代理商1、2 的级别
                        b.GameId == gameId && b.UserId == userId
                 group a by new { a.GameId, a.UserId, a.MyAgentLevel, a.NickName, a.ParentUserId } into g
                 select new
                 {
                     GameId = g.Key.GameId,
                     UserId = g.Key.UserId,
                     MyAgentLevel = g.Key.MyAgentLevel,
                     NickName = g.Key.NickName,
                     ParentUserId = g.Key.ParentUserId
                 }).ToList();

            return listPreviousPromoter.Select(a => new User
            {
                GameId = a.GameId,
                UserId = a.UserId,
                MyAgentLevel = a.MyAgentLevel,
                NickName = a.NickName,
                ParentUserId = a.ParentUserId
            }).ToList();
        }
    }
}
