using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Creeper.Tools;
using Entity;
using Entity.Base;

namespace Console
{
    class Program
    {
        //static System.Windows.Forms.WebBrowser wb;
        private static string exLogFile = "___Exception";
        static MyClass _mc;
        static CreeperService _cs;
        static DbHelper _db;

        //[STAThread]
        private static void Main(string[] args)
        {
            string strFileName = "___Creeper";
            LogHelper.WriteToLog("日志测试：", strFileName);
            _mc = new MyClass();
            _cs = new CreeperService();
            _db = new DbHelper();

            //cs.GetValidateImage(ref mc);

            Login();

            //GetAllProduct();
            //GetAllAgentLevel();

            //GetAllAgent();
            //GetAllUser();

            GetCharge();
            SalesmanRebate();

            //_cs.EachProductAgentCountDistributionDetail(ref mc);
            //_cs.EachProductUserDistributionDetail(ref mc);
            //_cs.DayRebatesAnalysis(ref mc);
        }

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
                    var entityDb = rep.Get(A => A.AccountId == Entity.AccountId).FirstOrDefault();
                    if (entityDb == null)
                        rep.Insert(Entity);
                    else
                        rep.Update(Entity);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[用户登录异常]:" + e, exLogFile);
            }
        }

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
                        var entityDb = rep.Get(A => A.GameId == Entity.GameId).FirstOrDefault();
                        if (entityDb == null)
                            listInsert.Add(Entity);
                        else
                        {
                            entityDb.GameId = Entity.GameId;
                            entityDb.GameName = Entity.GameName;
                            entityDb.IsValid = Entity.IsValid;
                            entityDb.Remark = Entity.Remark;
                            entityDb.ServerApiUrl = Entity.ServerApiUrl;
                            entityDb.DBWanIP = Entity.DBWanIP;
                            entityDb.DBIntranetIP = Entity.DBIntranetIP;
                            entityDb.DBProt = Entity.DBProt;

                            listUpdate.Add(entityDb);
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
                        var entityDb = rep.Get(A => A.GameId == Entity.GameId).FirstOrDefault();
                        if (entityDb == null)
                            listInsert.Add(Entity);
                        else
                        {
                            entityDb.GameId = Entity.GameId;
                            entityDb.GameName = Entity.GameName;
                            entityDb.IsValid = Entity.IsValid;

                            entityDb.AgentLevelId = Entity.AgentLevelId;
                            entityDb.AgentLevelName = Entity.AgentLevelName;
                            entityDb.DProportion = Entity.DProportion;
                            entityDb.IProportion = Entity.IProportion;
                            entityDb.IProportion2 = Entity.IProportion2;

                            listUpdate.Add(entityDb);
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
                LogHelper.WriteToLog("[获取产品代理层级异常]:" + e, exLogFile);
            }
        }

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
                        var entityDb = rep.Get(A => A.GameId == Entity.GameId && A.UserId == Entity.UserId).FirstOrDefault();
                        if (entityDb == null)
                            listInsert.Add(Entity);
                        else
                        {
                            entityDb.GameId = Entity.GameId;
                            entityDb.GameName = Entity.GameName;
                            entityDb.UserId = Entity.UserId;
                            entityDb.RealName = Entity.RealName;
                            entityDb.NickName = Entity.NickName;
                            entityDb.PhoneNo = Entity.PhoneNo;
                            entityDb.MyAgentLevel = Entity.MyAgentLevel;
                            entityDb.AgentLevelName = Entity.AgentLevelName;

                            entityDb.ParentUserId = Entity.ParentUserId;
                            entityDb.AgentLevel1 = Entity.AgentLevel1;
                            entityDb.AgentLevel2 = Entity.AgentLevel2;
                            //entityDb.AgentLevel3 = Entity.AgentLevel3;

                            entityDb.AgentCode = Entity.AgentCode;
                            entityDb.AgentStatus = Entity.AgentStatus;
                            entityDb.CreateTime = Entity.CreateTime;

                            //entityDb.DProportion = Entity.DProportion;
                            //entityDb.IProportion = Entity.IProportion;
                            //entityDb.IProportion2 = Entity.IProportion2;

                            listUpdate.Add(entityDb);
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
                LogHelper.WriteToLog("[获取代理商异常]:" + e, exLogFile);
            }
        }

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
                    var listSalesman = GetSalesman(resultEntity.rows);
                    var rep = new Repository<User>(_db);
                    foreach (var Entity in resultEntity.rows)
                    {
                        if (listSalesman.Count(A => A.GameId == Entity.GameId && A.UserId == Entity.UserId) > 0)
                        {
                            Entity.MyAgentLevel = 4;
                            Entity.UserType = "业务员";
                        }

                        #region 构造新增和修改的实体List
                        var entityDb = rep.Get(A => A.GameId == Entity.GameId && A.UserId == Entity.UserId).FirstOrDefault();
                        if (entityDb == null)
                            listInsert.Add(Entity);
                        else
                        {
                            entityDb.GameId = Entity.GameId;
                            entityDb.GameName = Entity.GameName;
                            entityDb.UserId = Entity.UserId;
                            entityDb.NickName = Entity.NickName;
                            entityDb.PhoneNo = Entity.PhoneNo;
                            entityDb.MyAgentLevel = Entity.MyAgentLevel;
                            entityDb.UserType = Entity.UserType;
                            entityDb.RegisterTime = Entity.RegisterTime;
                            entityDb.LastLoginTime = Entity.LastLoginTime;

                            entityDb.ParentUserId = Entity.ParentUserId;
                            entityDb.AgentRealName = Entity.AgentRealName;
                            entityDb.BindingTime = Entity.BindingTime;

                            entityDb.FriendID = Entity.FriendID;
                            entityDb.FriendNickName = Entity.FriendNickName;
                            entityDb.FBindingTime = Entity.FBindingTime;


                            entityDb.AgentLevel1 = Entity.AgentLevel1;
                            entityDb.AgentLevel2 = Entity.AgentLevel2;
                            entityDb.AgentLevel3 = Entity.AgentLevel3;

                            //entityDb.TotalCharge = Entity.TotalCharge;
                            //entityDb.TodayCharge = Entity.TodayCharge;
                            //entityDb.TotalRound = Entity.TotalRound;
                            //entityDb.TodayRound = Entity.TodayRound;
                            //entityDb.TotalWinRound = Entity.TotalWinRound;
                            //entityDb.TodayWinRound = Entity.TodayWinRound;
                            //entityDb.Diamond = Entity.Diamond;
                            //entityDb.Coin = Entity.Coin;
                            //entityDb.GameVersion = Entity.GameVersion;

                            listUpdate.Add(entityDb);
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
                LogHelper.WriteToLog("[获取用户异常]:" + e, exLogFile);
            }
        }

        private static List<User> GetSalesman(List<User> list)
        {
            var listNew =
                (from a in list
                 join b in list
                 on new { GameId = a.GameId, UserId = a.UserId } equals new { GameId = b.GameId, UserId = b.FriendID ?? 0 }
                 where a.MyAgentLevel == 0 && b.MyAgentLevel == 0
                 group a by new { a.GameId, a.UserId } into g
                 select new User { GameId = g.Key.GameId, UserId = g.Key.UserId }).ToList();
            return listNew;
        }

        private static void GetCharge()
        {
            try
            {
                string sTime = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:00:00");
                string eTime = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:59:59");
                var resultStr = _cs.InstantRebatesAnalysis(ref _mc, 0, sTime, eTime);
                var resultEntity = ToolsHelper._ConvertTools.JsonToObject<ResultModelPager<List<Charge>>>(resultStr);
                var listInsert = new List<Charge>();
                var listUpdate = new List<Charge>();

                if (resultEntity != null && resultEntity.rows != null)
                {
                    var rep = new Repository<Charge>(_db);
                    foreach (var Entity in resultEntity.rows)
                    {
                        #region MyRegion
                        var entityDb = rep.Get(A => A.SourceOrderId == Entity.SourceOrderId).FirstOrDefault();
                        if (entityDb == null)
                            listInsert.Add(Entity);
                        else
                        {
                            entityDb.GameId = Entity.GameId;
                            entityDb.GameName = Entity.GameName;

                            entityDb.SourceOrderId = Entity.SourceOrderId;
                            entityDb.OrderId = Entity.OrderId;
                            entityDb.PaymentId = Entity.PaymentId;

                            entityDb.OrderState = Entity.OrderState;
                            entityDb.IsReceive = Entity.IsReceive;

                            entityDb.ChargeUserId = Entity.ChargeUserId;
                            entityDb.NickName = Entity.NickName;

                            entityDb.UserId = Entity.UserId;
                            entityDb.RealName = Entity.RealName;

                            entityDb.RebateAmount = Entity.RebateAmount;
                            entityDb.ChargeAmount = Entity.ChargeAmount;
                            entityDb.DProportion = Entity.DProportion;
                            entityDb.Writedate = Entity.Writedate;

                            listUpdate.Add(entityDb);
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

        private static void SalesmanRebate()
        {
            try
            {
                var listInsert = new List<Charge>();

                var sTime = Convert.ToDateTime(DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:00:00"));
                var eTime = Convert.ToDateTime(DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:59:59"));
                var rep = new Repository<Charge>(_db);
                var repUser = new Repository<User>(_db);
                var repALevel = new Repository<AgentLevel>(_db);

                // 获取非本平台在指定时间段提交的订单
                var resultList = rep.Get(A =>
                    A.Writedate >= sTime &&
                    A.Writedate <= eTime && A.Platform == 0).ToList();

                foreach (var charge in resultList)
                {
                    // 若原平台订单已经生成 业务员返利订单，则跳过
                    //var temp = .ToList();
                    if (rep.Get(A => A.OldSourceOrderId == charge.SourceOrderId && A.Platform == 1).Any())
                        continue;

                    // 获取当前充值账户是否存在上级 业务员
                    var listNew =
                        (from a in repUser.Get()
                        join b in repUser.Get()
                        on new { GameId = a.GameId, UserId = a.UserId } equals new { GameId = b.GameId, UserId = b.FriendID ?? 0 }
                        where a.MyAgentLevel == 4 //&& b.MyAgentLevel == 0
                        && b.GameId == charge.GameId && b.UserId == charge.ChargeUserId 
                        group a by new { a.GameId, a.UserId } into g
                        select new { GameId = g.Key.GameId, UserId = g.Key.UserId }).ToList();

                    // 该充值用户 charge.ChargeUserId 有上级业务员时
                    if (listNew.Count == 1)
                    {
                        // 获得上级业务员
                        var tempUser = listNew.FirstOrDefault();
                        var salesman = repUser.Get(A => A.GameId == tempUser.GameId && A.UserId == tempUser.UserId).FirstOrDefault();

                        // 获得业务员应得返利的比例
                        var agentLevelEntity = repALevel.Get(A =>
                            A.GameId == salesman.GameId && A.AgentLevelId == salesman.MyAgentLevel).FirstOrDefault();

                        // 返利的比例
                        var dProportion = agentLevelEntity != null ? agentLevelEntity.DProportion : 0;

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

                            UserId = salesman!=null?salesman.UserId:0,
                            RealName = salesman!=null?salesman.NickName:"",

                            Writedate = DateTime.Now,
                            Platform = 1,
                            OldSourceOrderId = charge.SourceOrderId
                        };

                        listInsert.Add(waitRebateEntity);
                    }
                    else if (listNew.Count > 1)
                    {
                        LogHelper.WriteToLog("[业务员返利记录异常]: 充值用户的上级业务员出现了2及以上", exLogFile);
                    }
                }

                // 批量新增
                if (listInsert.Count > 0)
                    rep.Insert(listInsert);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[业务员返利记录异常]:" + e, exLogFile);
            }
        }
    }
}
