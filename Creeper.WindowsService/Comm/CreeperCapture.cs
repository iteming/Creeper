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
        private static string exLogFile = "___Exception";
        static MyClass _mc;
        static DataCaptureService _cs;
        static DbHelper _db;

        /// <summary>
        /// 执行数据抓取
        /// </summary>
        public static void doCapture()
        {
            _mc = new MyClass();
            _cs = new DataCaptureService();
            _db = new DbHelper();

            string strFileName = "___Creeper";
            LogHelper.WriteToLog("执行数据抓取 -- 开始：", strFileName);

            Login();
            GetAllProduct();
            GetAllAgentLevel();
            GetAllAgentApply();
            GetAllAgent();
            GetAllUser();
            GetCharge();
            //PromoterRebate();

            //_cs.GetValidateImage(ref mc);
            //_cs.EachProductAgentCountDistributionDetail(ref mc);
            //_cs.EachProductUserDistributionDetail(ref mc);
            //_cs.DayRebatesAnalysis(ref mc);
        }

        /// <summary>
        /// 登录 获取 ASP.NET_SessionID
        /// </summary>
        private static void Login()
        {
            try
            {
                var resultStr = _cs.Login(ref _mc);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModel<List<Admin>>>(resultStr);
                // 登录成功
                if (resultEntity.Ret == 1 && resultEntity.Data != null)
                {
                    var Entity = resultEntity.Data.FirstOrDefault();
                    var rep = new Repository<Admin>(_db);
                    var dbEntity = rep.Get(A => A.AccountId == Entity.AccountId).FirstOrDefault();
                    if (string.IsNullOrEmpty(Entity.Password))
                        Entity.Password = "789453f7d1f78abe3711e80dba307728";

                    if (dbEntity == null)
                        rep.Insert(Entity);
                    else
                    {
                        if (string.IsNullOrEmpty(dbEntity.Password))
                            dbEntity.Password = Entity.Password;

                        dbEntity.AccountId = Entity.AccountId;
                        dbEntity.IsPrimary = Entity.IsPrimary;
                        dbEntity.RoleId = Entity.RoleId;
                        dbEntity.RoleName = Entity.RoleName;
                        dbEntity.UserName = Entity.UserName;

                        rep.Update(dbEntity);
                    }
                        
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[用户登录异常]:" + e, exLogFile);
            }
        }

        /// <summary>
        /// 获取所有产品
        /// </summary>
        private static void GetAllProduct()
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
                    foreach (var Entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(A => A.GameId == Entity.GameId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(Entity);
                        else
                        {
                            dbEntity.GameId = Entity.GameId;
                            dbEntity.GameName = Entity.GameName;
                            dbEntity.IsValid = Entity.IsValid;
                            dbEntity.Remark = Entity.Remark;
                            dbEntity.ServerApiUrl = Entity.ServerApiUrl;
                            dbEntity.DBWanIP = Entity.DBWanIP;
                            dbEntity.DBIntranetIP = Entity.DBIntranetIP;
                            dbEntity.DBProt = Entity.DBProt;

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
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取产品异常]:" + e, exLogFile);
            }
        }

        /// <summary>
        /// 获取所有产品分利信息
        /// </summary>
        private static void GetAllAgentLevel()
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
                    foreach (var Entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(A => A.GameId == Entity.GameId && A.AgentLevelId == Entity.AgentLevelId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(Entity);
                        else
                        {
                            dbEntity.GameId = Entity.GameId;
                            dbEntity.GameName = Entity.GameName;
                            dbEntity.IsValid = Entity.IsValid;

                            dbEntity.AgentLevelId = Entity.AgentLevelId;
                            dbEntity.AgentLevelName = Entity.AgentLevelName;
                            dbEntity.DProportion = Entity.DProportion;
                            dbEntity.IProportion = Entity.IProportion;
                            dbEntity.IProportion2 = Entity.IProportion2;

                            listUpdate.Add(dbEntity);
                        }
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
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取产品代理层级异常]:" + e, exLogFile);
            }
        }

        /// <summary>
        /// 获取所有代理
        /// </summary>
        private static void GetAllAgent()
        {
            try
            {
                var resultStr = _cs.GetAllAgent(ref _mc);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<Agent>>>(resultStr);
                var listInsert = new List<Agent>();
                var listUpdate = new List<Agent>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    var rep = new Repository<Agent>(_db);
                    foreach (var Entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(A => A.GameId == Entity.GameId && A.UserId == Entity.UserId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(Entity);
                        else
                        {
                            // 对方平台审核通过，成为代理后。是否替换本系统审核通过成为推广员的信息
                            if (dbEntity.Platform == 1 && !Convert.ToBoolean(ConfigurationManager.AppSettings["ReplaceAgent"]))
                                continue;

                            dbEntity.GameId = Entity.GameId;
                            dbEntity.GameName = Entity.GameName;
                            dbEntity.UserId = Entity.UserId;
                            dbEntity.RealName = Entity.RealName;
                            dbEntity.NickName = Entity.NickName;
                            dbEntity.PhoneNo = Entity.PhoneNo;
                            dbEntity.MyAgentLevel = Entity.MyAgentLevel;
                            dbEntity.AgentLevelName = Entity.AgentLevelName;

                            dbEntity.ParentUserId = Entity.ParentUserId;
                            dbEntity.AgentLevel1 = Entity.AgentLevel1;
                            dbEntity.AgentLevel2 = Entity.AgentLevel2;
                            //dbEntity.AgentLevel3 = Entity.AgentLevel3;

                            dbEntity.AgentCode = Entity.AgentCode;
                            dbEntity.AgentStatus = Entity.AgentStatus;
                            dbEntity.CreateTime = Entity.CreateTime;

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
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取代理异常]:" + e, exLogFile);
            }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        private static void GetAllUser()
        {
            try
            {
                var resultStr = _cs.GetAllUser(ref _mc);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<User>>>(resultStr);
                var listInsert = new List<User>();
                var listUpdate = new List<User>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    //// 根据规则获取所有推广员
                    //var listPromoter = GetPromoterOld(resultEntity.rows);
                    resultEntity.rows = GetPromoter(resultEntity.rows);

                    var rep = new Repository<User>(_db);
                    foreach (var Entity in resultEntity.rows)
                    {
                        //// 循环的本次用户 Entity 是否是推广员
                        //if (listPromoter.Count(A => A.GameId == Entity.GameId && A.UserId == Entity.UserId) > 0)
                        //{
                        //    Entity.MyAgentLevel = ConstClass.PromoterLevelId;
                        //    Entity.UserType = ConstClass.PromoterLevelName;
                        //}

                        #region 构造新增和修改的实体List
                        var dbEntity = rep.Get(A => A.GameId == Entity.GameId && A.UserId == Entity.UserId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(Entity);
                        else
                        {
                            if (dbEntity.Platform == 1)
                                continue;
                            
                            dbEntity.GameId = Entity.GameId;
                            dbEntity.GameName = Entity.GameName;
                            dbEntity.UserId = Entity.UserId;
                            dbEntity.NickName = Entity.NickName;
                            dbEntity.PhoneNo = Entity.PhoneNo;
                            dbEntity.MyAgentLevel = Entity.MyAgentLevel;
                            dbEntity.UserType = Entity.UserType;
                            dbEntity.RegisterTime = Entity.RegisterTime;
                            dbEntity.LastLoginTime = Entity.LastLoginTime;

                            dbEntity.ParentUserId = Entity.ParentUserId;
                            dbEntity.AgentRealName = Entity.AgentRealName;
                            dbEntity.BindingTime = Entity.BindingTime;

                            dbEntity.FriendID = Entity.FriendID;
                            dbEntity.FriendNickName = Entity.FriendNickName;
                            dbEntity.FBindingTime = Entity.FBindingTime;


                            dbEntity.AgentLevel1 = Entity.AgentLevel1;
                            dbEntity.AgentLevel2 = Entity.AgentLevel2;
                            dbEntity.AgentLevel3 = Entity.AgentLevel3;

                            //dbEntity.TotalCharge = Entity.TotalCharge;
                            //dbEntity.TodayCharge = Entity.TodayCharge;
                            //dbEntity.TotalRound = Entity.TotalRound;
                            //dbEntity.TodayRound = Entity.TodayRound;
                            //dbEntity.TotalWinRound = Entity.TotalWinRound;
                            //dbEntity.TodayWinRound = Entity.TodayWinRound;
                            //dbEntity.Diamond = Entity.Diamond;
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
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取用户异常]:" + e, exLogFile);
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
             * 用户A和用户B的用户级别 MyAgentLevel = 0 (普通用户)
             * 如果用户B的 FriendID (好友ID) = 用户A.UserId 
             * 则用户A 属于推广员
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
            var levelList = rep.Get().OrderBy(A=> A.GameId).ThenBy(A => A.AgentLevelId);

            foreach (var level in levelList)
            {
                // 三级以上的两个等级不筛选
                if (level.AgentLevelId < 3)
                    continue;

                // 本次应该查找的用户等级
                var thisLevelId = level.AgentLevelId != 3 ? level.AgentLevelId - 1 : level.AgentLevelId;

                // 查找 父级为本次查找等级 的用户id
                var userArray =
                    (from b in userList
                     join a in userList
                        on new { GameId = b.GameId, UserId = b.ParentUserId ?? 0 } equals new { GameId = a.GameId, UserId = a.UserId } 
                     where a.GameId == level.GameId && a.MyAgentLevel == thisLevelId && b.MyAgentLevel == thisLevelId
                    group b by b.UserId into g
                    select new { userid = g.Key }).Select(A => A.userid).ToArray();

                // 根据用户id查找具体的用户
                var thisUserList = userList.Where(A => A.GameId == level.GameId && userArray.Contains(A.UserId)).ToList();

                // 修改这些用户的代理等级
                foreach (var user in thisUserList)
                {
                    user.MyAgentLevel = level.AgentLevelId;
                    user.UserType = level.AgentLevelName;
                }
            }
            return userList;
        }

        /// <summary>
        /// 获取前一个小时之内的所有充值返利订单
        /// </summary>
        private static void GetCharge()
        {
            try
            {
                var rep = new Repository<Charge>(_db);

                var sTime = "1900-01-01 00:00:00";
                var eTime = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:00:00");

                #region 历史订单只获取一次
                // 获取非本平台在指定时间段提交的订单
                var tempSTime = Convert.ToDateTime(sTime);
                var tempETime = Convert.ToDateTime(eTime);
                var historyCount = rep.Get(A => A.Writedate >= tempSTime && A.Writedate <= tempETime && A.Platform == 0).Count();
                if (historyCount > 0)
                {
                    sTime = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:00:00");
                    eTime = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:59:59");
                }
                #endregion

                var resultStr = _cs.InstantRebatesAnalysis(ref _mc, 0, sTime, eTime);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<Charge>>>(resultStr);
                var listInsert = new List<Charge>();
                var listUpdate = new List<Charge>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    foreach (var Entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(A => A.SourceOrderId == Entity.SourceOrderId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(Entity);
                        else
                        {
                            dbEntity.GameId = Entity.GameId;
                            dbEntity.GameName = Entity.GameName;

                            dbEntity.SourceOrderId = Entity.SourceOrderId;
                            dbEntity.OrderId = Entity.OrderId;
                            dbEntity.PaymentId = Entity.PaymentId;

                            dbEntity.OrderState = Entity.OrderState;
                            dbEntity.IsReceive = Entity.IsReceive;

                            dbEntity.ChargeUserId = Entity.ChargeUserId;
                            dbEntity.NickName = Entity.NickName;

                            dbEntity.UserId = Entity.UserId;
                            dbEntity.RealName = Entity.RealName;

                            dbEntity.RebateAmount = Entity.RebateAmount;
                            dbEntity.ChargeAmount = Entity.ChargeAmount;
                            dbEntity.DProportion = Entity.DProportion;
                            dbEntity.Writedate = Entity.Writedate;

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
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取订单异常]:" + e, exLogFile);
            }
        }

        /// <summary>
        /// 根据前一个小时内的充值返利订单 
        /// 构造符合条件的推广员返利订单
        /// </summary>
        private static void PromoterRebate()
        {
            try
            {
                var rep = new Repository<Charge>(_db);
                var repUser = new Repository<User>(_db);
                var repALevel = new Repository<AgentLevel>(_db);
                var listInsert = new List<Charge>();


                var sTime = Convert.ToDateTime("1900-01-01 00:00:00");
                var eTime = Convert.ToDateTime(DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:00:00"));

                #region 历史订单只获取一次
                // 获取非本平台在指定时间段提交的订单
                var historyCount = rep.Get(A => A.Writedate >= sTime && A.Writedate <= eTime && A.Platform == 1).Count();
                if (historyCount > 0)
                {
                    sTime = Convert.ToDateTime(DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:00:00"));
                    eTime = Convert.ToDateTime(DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:59:59"));
                }
                #endregion

                // 获取非本平台在指定时间段提交的订单
                var resultList = rep.Get(A => A.Writedate >= sTime && A.Writedate <= eTime && A.Platform == 0).ToList();
                foreach (var charge in resultList)
                {
                    // 若原平台订单已经生成 推广员返利订单，则跳过
                    if (rep.Get(A => A.OldSourceOrderId == charge.SourceOrderId && A.Platform == 1).Any())
                        continue;

                    #region 下级推广员
                    //var listNextPromoter =
                    //    (from b in repUser.Get()
                    //     join a in repUser.Get()
                    //     on new { GameId = b.GameId, UserId = b.ParentUserId ?? 0 } equals new { GameId = a.GameId, UserId = a.UserId }
                    //     where b.MyAgentLevel >= ConstClass.PromoterLevelId && 
                    //            a.GameId == charge.GameId && a.UserId == charge.ChargeUserId
                    //     group b by new { b.GameId, b.UserId } into g
                    //     select new { GameId = g.Key.GameId, UserId = g.Key.UserId }).ToList();
                    #endregion

                    #region 上级推广员
                    // 获取当前充值账户是否存在上级 推广员
                    var listPreviousPromoter =
                        (from a in repUser.Get()
                         join b in repUser.Get()
                         on new { GameId = a.GameId, UserId = a.UserId } equals new { GameId = b.GameId, UserId = b.ParentUserId ?? 0 } 
                         where a.MyAgentLevel >= ConstClass.PromoterLevelId &&
                                b.GameId == charge.GameId && b.UserId == charge.ChargeUserId
                         group a by new { a.GameId, a.UserId, a.MyAgentLevel, a.NickName } into g
                         select new { GameId = g.Key.GameId, UserId = g.Key.UserId,
                             MyAgentLevel = g.Key.MyAgentLevel, NickName= g.Key.NickName }).ToList();
                    #endregion

                    // 该充值用户 charge.ChargeUserId 有上级推广员时
                    if (listPreviousPromoter.Count == 1)
                    {
                        // 获得上级推广员
                        var promoter = listPreviousPromoter.FirstOrDefault();

                        // 获得推广员应得返利的比例
                        var agentLevelEntity = repALevel.Get(A => A.GameId == promoter.GameId 
                            && A.AgentLevelId == promoter.MyAgentLevel).FirstOrDefault();

                        // 如果该用户的返利比例信息未找到，则跳过
                        if (agentLevelEntity == null)
                        {
                            LogHelper.WriteToLog("[推广员返利记录错误]: 用户的返利比例信息未找到", exLogFile);
                            continue;
                        }

                        // 返利的比例
                        var dProportion = agentLevelEntity.DProportion;

                        // 返利的金额
                        var rebateAmount = charge.ChargeAmount * dProportion;

                        // 创建返利的记录
                        var waitRebateEntity = new Charge
                        {
                            ChargeAmount = charge.ChargeAmount,
                            ChargeUserId = charge.ChargeUserId,
                            NickName = charge.NickName,

                            DProportion = dProportion,
                            RebateAmount = rebateAmount,

                            GameId = charge.GameId,
                            GameName = charge.GameName,
                            IsReceive = 0,
                            OrderState = 0,

                            SourceOrderId = DateTime.Now.Ticks.ToString(),
                            OrderId = "",
                            PaymentId = "",

                            UserId = promoter != null ? promoter.UserId : 0,
                            RealName = promoter != null ? promoter.NickName : "",

                            Writedate = DateTime.Now,
                            Platform = 1,
                            OldSourceOrderId = charge.SourceOrderId
                        };

                        listInsert.Add(waitRebateEntity);
                    }
                    else if (listPreviousPromoter.Count > 1)
                    {
                        LogHelper.WriteToLog("[推广员返利记录错误]: 充值用户的上级推广员出现了2个及以上", exLogFile);
                    }
                }

                // 批量新增
                if (listInsert.Count > 0)
                    rep.Insert(listInsert);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[推广员返利记录异常]:" + e, exLogFile);
            }
        }


        /// <summary>
        /// 获取代理商申请
        /// </summary>
        private static void GetAllAgentApply()
        {
            try
            {
                var resultStr = _cs.GetAllAgentApply(ref _mc, 0);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<AgentApply>>>(resultStr);
                var listInsert = new List<AgentApply>();
                var listUpdate = new List<AgentApply>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    var rep = new Repository<AgentApply>(_db);
                    foreach (var Entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var dbEntity = rep.Get(A => A.GameId == Entity.GameId && A.UserId == Entity.UserId).FirstOrDefault();
                        if (dbEntity == null)
                            listInsert.Add(Entity);
                        else
                        {
                            if (dbEntity.Platform == 1)
                                continue;

                            dbEntity.GameId = Entity.GameId;
                            dbEntity.GameName = Entity.GameName;

                            dbEntity.UserId = Entity.UserId;
                            dbEntity.RealName = Entity.RealName;
                            dbEntity.NickName = Entity.NickName;
                            dbEntity.PhoneNo = Entity.PhoneNo;

                            dbEntity.RegisterTime = Entity.RegisterTime;
                            dbEntity.GameRounds = Entity.GameRounds;
                            dbEntity.RoomCardUsed = Entity.RoomCardUsed;

                            dbEntity.ApplyTime = Entity.ApplyTime;
                            dbEntity.AuditTime = Entity.AuditTime;
                            dbEntity.PassFlag = Entity.PassFlag;
                            dbEntity.Remark = Entity.Remark;

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
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取代理申请]:" + e, exLogFile);
            }
        }
    }
}
