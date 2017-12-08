using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.Tools;
using Entity;
using Entity.Base;
using Entity.Param;

namespace Service
{
    public class AllService
    {
        private static string exLogFile = "Bissness\\___Exception";
        private static DbHelper _db;

        public AllService()
        {
            _db = new DbHelper();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public ResultModel<Admin> Login(ParamLogin param)
        {
            try
            {
                var rep = new Repository<Admin>(_db);

                var dbEntity = rep.Get(A => A.UserName == param.UserName).FirstOrDefault();
                if (dbEntity != null && dbEntity.Password == MD5Helper.ToMd5Bit32(param.Password))
                    return ConstClass.Success.SetResult(dbEntity);

                return "登录失败,帐号或密码错误!".SetResult<Admin>(null);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[登录异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult<Admin>(null);
            }
        }


        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public ResultModel<List<Product>> GetProduct(ParamProduct param)
        {
            try
            {
                var rep = new Repository<Product>(_db);

                Expression<Func<Product, bool>> filter = null;
                if (param.GameId != 0)
                    filter = f => f.GameId == param.GameId;
                if (!string.IsNullOrEmpty(param.GameName))
                    filter = f => f.GameName.Contains(param.GameName);

                var list = filter != null ?
                            rep.Get(filter).ToList() :
                            rep.Get().ToList();

                return ConstClass.Success.SetResult(list);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult<List<Product>>(null);
            }
        }

        /// <summary>
        /// 查询所有产品分利层级
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public ResultModelPager<List<AgentLevel>> GetAgentLevel(ParamAgentLevel param)
        {
            try
            {
                var rep = new Repository<AgentLevel>(_db);

                Expression<Func<AgentLevel, bool>> filter = null;
                if (param.GameId != 0)
                    filter = f => f.GameId == param.GameId;
                if (!string.IsNullOrEmpty(param.GameName))
                    filter = f => f.GameName.Contains(param.GameName);
                if (param.AgentLevelId != 0)
                    filter = f => f.AgentLevelId == param.AgentLevelId;
                if (!string.IsNullOrEmpty(param.AgentLevelName))
                    filter = f => f.AgentLevelName.Contains(param.AgentLevelName);

                var list = filter != null ?
                            rep.Get(filter).ToList() :
                            rep.Get().ToList();

                var count = filter != null ?
                            rep.Get(filter).Count() :
                            rep.Get().Count();

                return ConstClass.Success.SetResultPager(list, count, param.PageIndex, param.PageSize);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResultPager<List<AgentLevel>>(null);
            }
        }

        /// <summary>
        /// 更新产品分利层级（新增、修改）
        /// </summary>
        /// <param name="entity">待更新的实体</param>
        /// <returns></returns>
        public ResultModel UpdateAgentLevel(AgentLevel entity)
        {
            try
            {
                var rep = new Repository<AgentLevel>(_db);
                var dbEntity = rep.Get(A=>A.GameId == entity.GameId && A.AgentLevelId == entity.AgentLevelId).FirstOrDefault();

                if (dbEntity == null)
                    rep.Insert(entity);
                else
                {
                    // 修改
                    dbEntity.GameId = entity.GameId;
                    dbEntity.GameName = entity.GameName;
                    dbEntity.IsValid = entity.IsValid;

                    dbEntity.AgentLevelId = entity.AgentLevelId;
                    dbEntity.AgentLevelName = entity.AgentLevelName;
                    dbEntity.DProportion = entity.DProportion;
                    dbEntity.IProportion = entity.IProportion;
                    dbEntity.IProportion2 = entity.IProportion2;
                    rep.Update(dbEntity);
                }

                return ConstClass.Success.SetResult("TRUE");
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult(null);
            }
        }


        /// <summary>
        /// 查询所有代理
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public ResultModel<List<Agent>> GetAgent(ParamUserAgent param)
        {
            try
            {
                var rep = new Repository<Agent>(_db);

                Expression<Func<Agent, bool>> filter = null;
                if (param.GameId != 0)
                    filter = f => f.GameId == param.GameId;
                if (!string.IsNullOrEmpty(param.GameName))
                    filter = f => f.GameName.Contains(param.GameName);

                if (!string.IsNullOrEmpty(param.UserKey))
                    filter = f => f.UserId == Convert.ToInt32(param.UserKey) ||
                                  f.RealName.Contains(param.UserKey) ||
                                  f.NickName.Contains(param.UserKey) ||
                                  f.PhoneNo.Contains(param.UserKey);
                if (param.MyAgentLevel != 0)
                    filter = f => f.MyAgentLevel == param.MyAgentLevel;

                var list = filter != null ?
                            rep.Get(filter).ToList() :
                            rep.Get().ToList();

                return ConstClass.Success.SetResult(list);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult<List<Agent>>(null);
            }
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public ResultModel<List<User>> GetUser(ParamUserAgent param)
        {
            try
            {
                var rep = new Repository<User>(_db);

                Expression<Func<User, bool>> filter = null;
                if (param.searchPromoter)
                    filter = f => f.MyAgentLevel >= ConstClass.PromoterLevelId;

                if (param.GameId != 0)
                    filter = f => f.GameId == param.GameId;
                if (!string.IsNullOrEmpty(param.GameName))
                    filter = f => f.GameName.Contains(param.GameName);

                if (!string.IsNullOrEmpty(param.UserKey))
                    filter = f => f.UserId == Convert.ToInt32(param.UserKey) ||
                                  f.NickName.Contains(param.UserKey) ||
                                  f.PhoneNo.Contains(param.UserKey);
                if (param.MyAgentLevel != 0)
                    filter = f => f.MyAgentLevel == param.MyAgentLevel;

                var list = filter != null ?
                            rep.Get(filter).ToList() :
                            rep.Get().ToList();

                return ConstClass.Success.SetResult(list);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult<List<User>>(null);
            }
        }


        /// <summary>
        /// 查询所有充值订单
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        public ResultModel<List<Charge>> GetCharge(ParamUserAgent param)
        {
            try
            {
                var rep = new Repository<Charge>(_db);

                Expression<Func<Charge, bool>> filter = null;
                if (param.searchPromoter)
                    filter = f => f.Platform == 1;

                if (param.GameId != 0)
                    filter = f => f.GameId == param.GameId;
                if (!string.IsNullOrEmpty(param.GameName))
                    filter = f => f.GameName.Contains(param.GameName);

                if (!string.IsNullOrEmpty(param.UserKey))
                    filter = f => f.ChargeUserId == Convert.ToInt32(param.UserKey) ||
                                  f.NickName.Contains(param.UserKey);

                if (!string.IsNullOrEmpty(param.OrtherUserKey))
                    filter = f => f.UserId == Convert.ToInt32(param.OrtherUserKey) ||
                                  f.RealName.Contains(param.OrtherUserKey);

                var list = filter != null ?
                            rep.Get(filter).ToList() :
                            rep.Get().ToList();

                return ConstClass.Success.SetResult(list);
            }
            catch (Exception e)
            {
                LogHelper.WriteToLog("[异常]:" + e, exLogFile);
                return ConstClass.Exception.SetResult<List<Charge>>(null);
            }
        }
    }
}
