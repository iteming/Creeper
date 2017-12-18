using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Common.Tools;
using Common.WxModel;
using Entity;
using Entity.Base;
using Entity.Dto;
using Entity.Param;

namespace Service
{
    public class WxService
    {
        private static string exLogFile = "___Exception_WxBissness";
        private static DbHelper _db;

        public WxService()
        {
            _db = new DbHelper();
        }

        #region 暂不使用
        public Rebate GetRebateById(string OrderID)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Rebate> SubmitRebate(decimal money, string p)
        {
            throw new NotImplementedException();
        }
        #endregion

        public User Regist(WxUserInfo userEntity)
        {
            try
            {
                var rep = new Repository<User>(_db);
                var confGameId = Convert.ToInt32(ConfigurationManager.AppSettings["GameID"]);
                Expression<Func<User, bool>> filter = a => a.UnionId == "wx-"+userEntity.unionid && a.GameId == confGameId;
                var entity = rep.Get(filter).FirstOrDefault();

                if (entity == null)
                {
                    //filter = a => a.NickName == userEntity.nickname;
                    filter = a => a.Id == "10032|10028501"; // 贾昭凯
                    //filter = a => a.Id == "10013|10001205"; // 瑞普之夫
                    entity = rep.Get(filter).FirstOrDefault();
                }

                if (entity != null)
                {
                    entity.NickName = userEntity.nickname;
                    entity.Wx_Openid = userEntity.openid;
                    entity.HeaderUrl = userEntity.headimgurl;
                    rep.Update(entity);
                    return entity;
                }
                LogHelper.WriteToLog("[拉取网页授权信息错误]: 符合条件的用户unionid=" + userEntity.unionid + "不存在", exLogFile);
                return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[拉取网页授权信息异常]:" + e, exLogFile);
                return null;
            }
        }

        public ResultModel<User> AgentInfoLogin(int gameId, string mobile)
        {
            try
            {
                var rep = new Repository<User>(_db);
                Expression<Func<User, bool>> filter = a => a.GameId == gameId && a.PhoneNo == mobile;
                var entity = rep.Get(filter).FirstOrDefault();
                if (entity != null)
                    return ConstClass.Success.SetResult(entity);

                LogHelper.WriteToLog("[代理商登录错误]: 符合条件的游戏GameID=" + gameId + "用户PhoneNo=" + mobile + "不存在", exLogFile);
                return "非代理商号码".SetResult<User>(null);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[代理商登录异常]:" + e, exLogFile);
                return ConstClass.Failed.SetResult<User>(null);
            }
        }

        public User GetUserById(string id)
        {
            try
            {
                var rep = new Repository<User>(_db);
                Expression<Func<User, bool>> filter = a => a.Id == id;
                var entity = rep.Get(filter).FirstOrDefault();
                if (entity != null)
                    return entity;

                LogHelper.WriteToLog("[获取用户信息错误]: 符合条件的用户Id=" + id + "不存在", exLogFile);
                return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取用户信息异常]:" + e, exLogFile);
                return null;
            }
        }

        public Agent GetAgentById(string id)
        {
            try
            {
                var rep = new Repository<Agent>(_db);
                var entity = rep.Get(a => a.Id == id).FirstOrDefault();
                if (entity != null)
                    return entity;

                LogHelper.WriteToLog("[获取代理商信息错误]: 符合条件的用户Id=" + id + "不存在", exLogFile);
                return null;
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取代理商信息异常]:" + e, exLogFile);
                return null;
            }
        }

        public decimal GetCanRebateAmount(int gameid, int userid)
        {
            //收益统计
            try
            {
                var rep = new Repository<Rebate>(_db);
                Expression<Func<Rebate, bool>> filter = f => f.GameId == gameid && f.UserId == userid && f.OrderState == 0 && f.IsReceive == 0;
                var queryable = rep.Get(filter).ToList();

                // 累积收益
                return queryable.Sum(s => s.RebateAmount);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[收益统计异常]:" + e, exLogFile);
                return 0;
            }
        }

        public void GetRebateStatistics(int gameid, int userid, ref DtoRebateStatistics dtoRebate)
        {
            //收益统计
            try
            {
                var rep = new Repository<Rebate>(_db);
                Expression<Func<Rebate, bool>> filter = f => f.GameId == gameid && f.UserId == userid;
                // && f.OrderState == 1 && f.IsReceive == 1
                var queryable = rep.Get(filter);

                // 累积收益
                dtoRebate.TotalRebateAmount = queryable.Sum(s => s.RebateAmount);

                // 今日收益
                DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime dtTomorrow = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                filter = f => f.Writedate >= dtToday && f.Writedate < dtTomorrow;
                dtoRebate.TodayRebateAmount = queryable.Where(filter).ToList().Sum(s => s.RebateAmount);

                // 昨天收益
                DateTime dtYestoday = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
                filter = f => f.Writedate >= dtYestoday && f.Writedate < dtToday;
                dtoRebate.YestodayRebateAmount = queryable.Where(filter).ToList().Sum(s => s.RebateAmount);

                // 本周收益
                DateTime dtWeekFirstday = ConvertTools._ConvertTools.GetWeekFirstDayMon(DateTime.Now);
                filter = f => f.Writedate >= dtWeekFirstday && f.Writedate < dtTomorrow;
                dtoRebate.WeekRebateAmount = queryable.Where(filter).ToList().Sum(s => s.RebateAmount);

                // 当月收益
                DateTime dtMonthFirstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                filter = f => f.Writedate >= dtMonthFirstday && f.Writedate < dtTomorrow;
                dtoRebate.MonthRebateAmount = queryable.Where(filter).ToList().Sum(s => s.RebateAmount);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[收益统计异常]:" + e, exLogFile);
            }
        }

        public void GetUserCountStatistics(int gameid, int userid, ref DtoRebateStatistics dtoRebate)
        {
            //邀请用户
            try
            {
                var rep = new Repository<User>(_db);
                Expression<Func<User, bool>> filter = f => f.GameId == gameid && f.ParentUserId == userid;
                var queryable = rep.Get(filter);

                // 累积用户
                dtoRebate.TotalUserCount = queryable.Count();

                // 今日用户
                DateTime dtToday = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime dtTomorrow = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
                filter = f => f.BindingTime >= dtToday && f.BindingTime < dtTomorrow;
                dtoRebate.TodayUserCount = queryable.Where(filter).Count();

                // 昨天用户
                DateTime dtYestoday = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));
                filter = f => f.BindingTime >= dtYestoday && f.BindingTime < dtToday;
                dtoRebate.YestodayUserCount = queryable.Where(filter).Count();

                // 本周用户
                DateTime dtWeekFirstday = ConvertTools._ConvertTools.GetWeekFirstDayMon(DateTime.Now);
                filter = f => f.BindingTime >= dtWeekFirstday && f.BindingTime < dtTomorrow;
                dtoRebate.WeekUserCount = queryable.Where(filter).Count();

                // 当月用户
                DateTime dtMonthFirstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                filter = f => f.BindingTime >= dtMonthFirstday && f.BindingTime < dtTomorrow;
                dtoRebate.MonthUserCount = queryable.Where(filter).Count();
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[邀请用户数量统计异常]:" + e, exLogFile);
            }
        }

        public ResultModel<List<User>> GetUserCountList(string id, ParamUserList param)
        {
            try
            {
                var rep = new Repository<User>(_db);
                var entity = rep.Get(a => a.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    var queryable = rep.Get(f => f.GameId == entity.GameId && f.ParentUserId == entity.UserId);
                    if (!string.IsNullOrEmpty(param.BeginTime))
                    {
                        var beginTime = Convert.ToDateTime(param.BeginTime);
                        queryable = queryable.Where(a => a.BindingTime >= beginTime);
                    }

                    var list = queryable.OrderByDescending(o => o.BindingTime)
                        .Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize).ToList();
                    return ConstClass.Success.SetResult(list);
                }

                LogHelper.WriteToLog("[获取邀请用户列表错误]: 符合条件的用户Id=" + id + "不存在", exLogFile);
                return ConstClass.Failed.SetResult<List<User>>(null);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取邀请用户列表异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult<List<User>>(null);
            }
        }

        public ResultModel<List<DtoUserRanking>> GetUserRankingList(string id, ParamUserList param)
        {
            try
            {
                var rep = new Repository<User>(_db);
                var crep = new Repository<Rebate>(_db);
                var entity = rep.Get(a => a.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    var queryable = rep.Get(f => f.GameId == entity.GameId && f.ParentUserId == entity.UserId);
                    var result = (from b in crep.Get()
                                  join a in queryable
                                  on new { GameId = b.GameId, UserId = b.ChargeUserId } equals
                                      new { GameId = a.GameId, UserId = a.UserId }
                                  group b by new { Writedate = b.Writedate, UserId = b.ChargeUserId, NickName = b.NickName, ChargeAmount = b.ChargeAmount } into g
                                  select new DtoUserRanking
                                  {
                                      Writedate = g.Key.Writedate,
                                      UserId = g.Key.UserId,
                                      NickName = g.Key.NickName,
                                      ChargeAmount = g.Key.ChargeAmount,
                                      Rank = 1
                                  }).AsQueryable();

                    // 直属用户总充值金额
                    var totalUserChargeAmount = result.ToList().Sum(s => s.ChargeAmount);

                    if (param.Type == 1)
                    {
                        // 今日
                        var dtToday = DateTime.Now.ToString("yyyy-MM-dd");
                        var dtTomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                        param.BeginTime = dtToday;
                        param.EndTime = dtTomorrow;
                    }
                    else if (param.Type == 2)
                    {
                        // 本月
                        var dtMonthFirstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                        var dtTomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                        param.BeginTime = dtMonthFirstday;
                        param.EndTime = dtTomorrow;
                    }
                    else if (param.Type == 3)
                    {
                        // 上月
                        var dtMonthFirstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1).ToString("yyyy-MM-dd");
                        var dtMonthLastday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1).ToString("yyyy-MM-dd");
                        param.BeginTime = dtMonthFirstday;
                        param.EndTime = dtMonthLastday;
                    }

                    if (!string.IsNullOrEmpty(param.BeginTime))
                    {
                        var beginTime = Convert.ToDateTime(param.BeginTime);
                        result = result.Where(a => a.Writedate >= beginTime);
                    }
                    if (!string.IsNullOrEmpty(param.EndTime))
                    {
                        var endTime = Convert.ToDateTime(param.EndTime);
                        result = result.Where(a => a.Writedate <= endTime);
                    }

                    var resultEntityList = result.GroupBy(g => new { UserId = g.UserId, NickName = g.NickName })
                        .Select(g => new DtoUserRanking
                        {
                            Writedate = g.Max(m => m.Writedate), 
                            UserId = g.Key.UserId, 
                            NickName = g.Key.NickName,
                            ChargeAmount = g.Sum(s => s.ChargeAmount),
                            Rank = 1
                        }).AsQueryable();

                    var list = resultEntityList.OrderByDescending(o => o.ChargeAmount)
                        .Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize)
                        .ToList();

                    var ret = ConstClass.Success.SetResult(list);
                    ret.Temp = Convert.ToString(totalUserChargeAmount);
                    return ret;
                }

                LogHelper.WriteToLog("[获取邀请用户列表错误]: 符合条件的用户Id=" + id + "不存在", exLogFile);
                return ConstClass.Failed.SetResult<List<DtoUserRanking>>(null);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取邀请用户列表异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult<List<DtoUserRanking>>(null);
            }
        }

        public ResultModel<List<DtoUserGame>> UserGameGet(string id, ParamUserList param)
        {
            try
            {
                var rep = new Repository<User>(_db);
                var entity = rep.Get(a => a.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    var queryable = rep.Get(f => f.GameId == entity.GameId && f.ParentUserId == entity.UserId);

                    if (param.Type == 1)
                    {
                        // 今日
                        var dtToday = DateTime.Now.ToString("yyyy-MM-dd");
                        var dtTomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                        param.BeginTime = dtToday;
                        param.EndTime = dtTomorrow;
                    }
                    else if (param.Type == 2)
                    {
                        // 本月
                        var dtMonthFirstday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
                        var dtTomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                        param.BeginTime = dtMonthFirstday;
                        param.EndTime = dtTomorrow;
                    }

                    if (!string.IsNullOrEmpty(param.BeginTime))
                    {
                        var beginTime = Convert.ToDateTime(param.BeginTime);
                        queryable = queryable.Where(a => a.BindingTime >= beginTime);
                    }
                    if (!string.IsNullOrEmpty(param.EndTime))
                    {
                        var endTime = Convert.ToDateTime(param.EndTime);
                        queryable = queryable.Where(a => a.BindingTime <= endTime);
                    }

                    var resultEntityList = queryable.GroupBy(g => new { UserId = g.UserId, NickName = g.NickName })
                        .Select(g => new DtoUserGame
                        {
                            UserId = g.Key.UserId,
                            NickName = g.Key.NickName,
                            TotalRound = g.Sum(s => s.TotalRound),
                            TotalWinRound = g.Sum(s => s.TotalWinRound),
                            TodayRound = g.Sum(s => s.TodayRound),
                            TodayWinRound = g.Sum(s => s.TodayWinRound),
                            Diamond = g.Sum(s => s.Diamond)
                        }).AsQueryable();

                    var list = resultEntityList.OrderByDescending(o => o.Diamond)
                        .ThenByDescending(o => o.TodayRound).ThenByDescending(o => o.TotalRound)
                        .Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize)
                        .ToList();

                    return ConstClass.Success.SetResult(list);
                }

                LogHelper.WriteToLog("[获取邀请用户对局列表错误]: 符合条件的用户Id=" + id + "不存在", exLogFile);
                return ConstClass.Failed.SetResult<List<DtoUserGame>>(null);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[获取邀请用户对局列表异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult<List<DtoUserGame>>(null);
            }
        }
    }
}
