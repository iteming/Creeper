using System;
using System.Web.Mvc;
using Common;
using Common.Tools;
using Common.WxPay;
using Common.WxPay.Lib;
using Entity.Base;
using Service;

namespace Creeper.WX.Controllers
{
    public class WxPayController : Controller
    {
        //
        // GET: /WxPay/

        private static string exLogFile = "___Exception_WxBissness";

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="IdAmount">订单ID 或者 充值金额+"|"</param>
        /// <returns></returns>
        public ActionResult WxPayReady(string IdAmount)
        {
            JsApiPay jsApiPay = new JsApiPay(this);
            return Redirect(jsApiPay.WxCodeUrl("/WxPay/Default", IdAmount));
        }

        /// <summary>
        /// 充值回调
        /// </summary>
        /// <param name="code">回调code</param>
        /// <param name="state">回调携带的参数：IdAmount 订单ID 或者 充值金额+"|" </param>
        /// <returns></returns>
        public ActionResult Default(string code, string state)
        {
            WxService SVC = new WxService();
            Rebate recordEntity = new Rebate();
            ViewBag.tipStr = "微信支付正在处理...";

            if (!state.Contains("|"))
            {
                // 订单消费
                ViewBag.Flag = "";
                var OrderID = Convert.ToString(state);

                // 根据订单ID查询订单信息
                recordEntity = SVC.GetRebateById(OrderID);
                if (recordEntity == null)
                {
                    ViewBag.tipStr = "订单信息存在问题，请返回重试";
                    return View(recordEntity);
                }
                //// 测试订单金额 0.02
                //recordEntity.Amount = Convert.ToDecimal(0.02);
                if (recordEntity.ChargeAmount <= 0)
                {
                    ViewBag.tipStr = "支付金额必须大于0，请返回重试";
                    return View(recordEntity);
                }

                // 创建微信支付参数
                JsApiPay jsApiPay = new JsApiPay(this);
                jsApiPay.GetOpenidAndAccessTokenFromCode(code);
                jsApiPay.total_fee = Convert.ToInt32(recordEntity.ChargeAmount * 100);
                jsApiPay.order_no = recordEntity.OrderId;
                jsApiPay.attach = Convert.ToString(recordEntity.UserId);

                if (string.IsNullOrEmpty(jsApiPay.openid) || jsApiPay.total_fee <= 0)
                {
                    ViewBag.tipStr = "页面参数出错，请返回重试";
                    return View(recordEntity);
                }

                // JSAPI支付预处理
                try
                {
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                    string wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数       
                    ViewBag.wxJsApiParam = wxJsApiParam;
                }
                catch (Exception ex)
                {
                    ViewBag.tipStr = "下单失败，请返回重试";
                    LogHelper.WriteToLog("[下单失败]:" + SessionHelper._SessionHelper.UserName + ex, exLogFile);
                }
            }
            else
            {
                // 订单充值
                ViewBag.Flag = "1";

                // 判断充值金额
                var money = Convert.ToDecimal(state.Substring(0, state.Length - 1));
                if (money < Convert.ToDecimal(0.01))
                {
                    ViewBag.tipStr = "充值金额不能低于0.01";
                    return View(recordEntity);
                }

                // 创建充值订单
                var resultData = SVC.SubmitRebate(money, SessionHelper._SessionHelper.UserID);
                if (!(resultData != null && resultData.Ret > 0))
                {
                    ViewBag.tipStr = "创建充值订单失败";
                    return View(recordEntity);
                }

                // 取得订单信息
                recordEntity = resultData.Data;
                if (recordEntity.ChargeAmount <= 0)
                {
                    ViewBag.tipStr = "支付金额必须大于0，请返回重试";
                    return View(recordEntity);
                }

                // 创建微信支付参数
                JsApiPay jsApiPay = new JsApiPay(this);
                jsApiPay.GetOpenidAndAccessTokenFromCode(code);//openid
                jsApiPay.total_fee = Convert.ToInt32(recordEntity.ChargeAmount * 100);
                jsApiPay.order_no = recordEntity.OrderId;
                jsApiPay.attach = recordEntity.UserId.ToString() + "|";

                if (string.IsNullOrEmpty(jsApiPay.openid) || jsApiPay.total_fee <= 0)
                {
                    ViewBag.tipStr = "页面参数出错，请返回重试";
                    return View(recordEntity);
                }

                //JSAPI支付预处理
                try
                {
                    WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                    string wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数       
                    ViewBag.wxJsApiParam = wxJsApiParam;
                }
                catch (Exception ex)
                {
                    ViewBag.tipStr = "充值失败，请返回重试";
                    LogHelper.WriteToLog("[充值失败]:" + SessionHelper._SessionHelper.UserName + ex, exLogFile);
                }
            }
            return View(recordEntity);
        }

        public void WxPayNotify()
        {
            WxPayData res = new WxPayData();
            try
            {
                Notify notify = new Notify(this);
                WxPayData notifyData = notify.GetNotifyData();

                var orderNo = notifyData.GetValue("out_trade_no").ToString();
                var attach = notifyData.GetValue("attach").ToString(); // UserID 或者 UserID + "|"
                var resultCode = notifyData.GetValue("result_code").ToString();
                var otherOrderNum = notifyData.GetValue("transaction_id").ToString();
                var money = Convert.ToDecimal(notifyData.GetValue("total_fee").ToString());

                if (resultCode.ToUpper() == "SUCCESS")
                {
                    //// 修改订单状态
                    //ResultModel<Charge> result = new WxService().ModifyOrderStatus(orderNo, otherOrderNum);
                    //if (result.status > 0)
                    //{
                    //    if (result.data != null)
                    //    {
                    //        // 支付成功，修改订单状态成功之后
                    //        // 推送微信通知消息
                    //        SendMsg(result.data);
                    //    }
                    //    res.SetValue("return_code", "SUCCESS");
                    //    res.SetValue("return_msg", "OK");
                    //    Response.Write(res.ToXml());
                    //    Response.End();
                    //}
                }

                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "FAIL");
                Response.Write(res.ToXml());
                Response.End();
            }
            catch (Exception ex)
            {
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "FAIL");
                Response.Write(res.ToXml());
                Response.End();
                LogHelper.WriteToLog("[支付回调失败]:" + ex, exLogFile);
            }
        }

        public void SendMsg(Charge order)
        {
            try
            {
                //var SVC = new WxService();
                //var ConsumeRecharge = "购买商品成功。";

                ///// {{first.DATA}}
                ///// 游戏名：{{keyword1.DATA}}
                ///// 消费金额：{{keyword2.DATA}}
                ///// 订单号：{{keyword3.DATA}}
                ///// 时间：{{keyword4.DATA}}
                ///// {{remark.DATA}}
                //var toUser = SVC.GetUser(new ParamUserAgent { UserKey = Convert.ToString(order.UserId)});
                //JsApiPay.SendMsg(toUser.WeiXin_Openid, MsgConfig.Msg1, new
                //{
                //    first = new MsgValue() { value = "恭喜您，" + ConsumeRecharge },
                //    keyword1 = new MsgValue() { value = order.Description },
                //    keyword2 = new MsgValue() { value = Convert.ToString(order.Amount) + "元" },
                //    keyword3 = new MsgValue() { value = order.OrderNo },
                //    keyword4 = new MsgValue() { value = order.CreateTime.Value.ToString("yyyy-MM-dd") },
                //    remark = new MsgValue() { value = "中猎国际俱乐部祝您游戏愉快。遇到任何问题您可以随时联系公众号客服。客服电话：400-618-6816" },
                //});


                ///// {{first.DATA}}
                ///// 游戏名：{{keyword1.DATA}}
                ///// 消费金额：{{keyword2.DATA}}
                ///// 订单号：{{keyword3.DATA}}
                ///// 时间：{{keyword4.DATA}}
                ///// {{remark.DATA}}
                //var telephone = ConfigurationManager.AppSettings["ManagerPhone"].ToString();
                //var baseUser = SVC.GetUserBaseByTelePhone(telephone);
                //if (baseUser != null)
                //{
                //    JsApiPay.SendMsg(baseUser.WeiXin_Openid, MsgConfig.Msg1, new
                //    {
                //        first = new MsgValue() { value = string.Format("用户 {0} " + ConsumeRecharge, string.IsNullOrEmpty(toUser.TelePhone) ? toUser.UserName : toUser.TelePhone) },
                //        keyword1 = new MsgValue() { value = order.Description },
                //        keyword2 = new MsgValue() { value = Convert.ToString(order.Amount) + "元" },
                //        keyword3 = new MsgValue() { value = order.OrderNo },
                //        keyword4 = new MsgValue() { value = order.CreateTime.Value.ToString("yyyy-MM-dd") },
                //        remark = new MsgValue() { value = "恭喜您，财源滚滚！" },
                //    });
                //}
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog("[发送消息通知失败]:" + SessionHelper._SessionHelper.UserName + ex, exLogFile);
            }
        }


        public void WxRefundNotify()
        {
            WxPayData res = new WxPayData();
            try
            {
                Notify notify = new Notify(this);
                WxPayData notifyData = notify.GetRefundNotifyData();
                if (!notifyData.IsSet("return_code") || notifyData.GetValue("return_code").ToString() != "SUCCESS")
                {
                    goto TheEnd;
                }

                // 回调通知成功，取得密文
                var reqCiphertext = notifyData.GetValue("req_info").ToString();
                // 解密
                var key = MD5Helper.ToMd5Bit32(PayConfig.WxKey());
                var clearText = MD5Helper.AESDecrypt(reqCiphertext, key);

                //转换数据格式
                WxPayData reqCipherData = new WxPayData();
                try { reqCipherData.FromXml(clearText, false); }
                catch (WxPayException) { }

                var refundStatus = reqCipherData.GetValue("refund_status").ToString();
                var otherRefundNum = reqCipherData.GetValue("out_refund_no").ToString();
                var refundNo = reqCipherData.GetValue("out_trade_no").ToString();

                if (refundStatus.ToUpper() == "SUCCESS")
                {
                    // 修改订单状态
                    //ResultModel<Charge> result = new WxService().ModifyOrderStatusRefund(refundNo, otherRefundNum);
                    //if (result.status > 0)
                    //{
                    //    if (result.data != null)
                    //    {
                    //        // 修改退款单状态
                    //        new RefundService().UpdateRefundStatus(result.data.RecordID, otherRefundNum, Order_Status.Refunded);
                    //        // 退款成功，修改订单状态成功之后
                    //        // 推送微信通知消息
                    //        SendMsgRefund(result.data);
                    //    }
                    //    res.SetValue("return_code", "SUCCESS");
                    //    res.SetValue("return_msg", "OK");
                    //    Response.Write(res.ToXml());
                    //    Response.End();
                    //    return;
                    //}
                }

            TheEnd:
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "FAIL");
                Response.Write(res.ToXml());
                Response.End();
            }
            catch (Exception ex)
            {
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "FAIL");
                Response.Write(res.ToXml());
                Response.End();
                LogHelper.WriteToLog("[退款回调失败]:" + ex, exLogFile);
            }
        }

        public void SendMsgRefund(Charge order)
        {
            try
            {
                //var SVC = new WxService();;
                //var ConsumeRecharge = "消费退款成功。";

                ///// {{first.DATA}}
                ///// 游戏名：{{keyword1.DATA}}
                ///// 消费金额：{{keyword2.DATA}}
                ///// 订单号：{{keyword3.DATA}}
                ///// 时间：{{keyword4.DATA}}
                ///// {{remark.DATA}}
                //var toUser = SVC.GetUser(new ParamUserAgent { UserKey = Convert.ToString(order.UserId) });
                //JsApiPay.SendMsg(toUser.WeiXin_Openid, MsgConfig.Msg2, new
                //{
                //    first = new MsgValue() { value = ConsumeRecharge },
                //    keyword1 = new MsgValue() { value = Convert.ToString(order.Amount) + "元" },
                //    keyword2 = new MsgValue() { value = order.OrderNo },
                //    keyword3 = new MsgValue() { value = order.CreateTime.Value.ToString("yyyy-MM-dd") },
                //    remark = new MsgValue() { value = "遇到任何问题您可以随时联系公众号客服。客服电话：400-618-6816" },
                //});


                ///// {{first.DATA}}
                ///// 游戏名：{{keyword1.DATA}}
                ///// 消费金额：{{keyword2.DATA}}
                ///// 订单号：{{keyword3.DATA}}
                ///// 时间：{{keyword4.DATA}}
                ///// {{remark.DATA}}
                //var telephone = ConfigurationManager.AppSettings["ManagerPhone"].ToString();
                //var baseUser = SVC.GetUserBaseByTelePhone(telephone);
                //if (baseUser != null)
                //{
                //    JsApiPay.SendMsg(baseUser.WeiXin_Openid, MsgConfig.Msg2, new
                //    {
                //        first = new MsgValue() { value = string.Format("用户 {0} " + ConsumeRecharge, string.IsNullOrEmpty(toUser.TelePhone) ? toUser.UserName : toUser.TelePhone) },
                //        keyword1 = new MsgValue() { value = Convert.ToString(order.Amount) + "元" },
                //        keyword2 = new MsgValue() { value = order.OrderNo },
                //        keyword3 = new MsgValue() { value = order.CreateTime.Value.ToString("yyyy-MM-dd") },
                //        remark = new MsgValue() { value = "如果对此有疑问，请联系技术人员！" },
                //    });
                //}
            }
            catch (Exception ex)
            {
                LogHelper.WriteToLog("[发送消息通知失败]:" + SessionHelper._SessionHelper.UserName + ex, exLogFile);
            }
        }
	}
}