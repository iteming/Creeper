using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    public class ErrorHelper
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public static KeyValuePair<int, string> Error_1 = new KeyValuePair<int, string>(1, "操作成功");
        /// <summary>
        /// 令牌验证未通过
        /// </summary>
        public static KeyValuePair<int, string> Error_11 = new KeyValuePair<int, string>(-11, "令牌验证未通过");
        /// <summary>
        /// 手机号码已存在
        /// </summary>
        public static KeyValuePair<int, string> Error_12 = new KeyValuePair<int, string>(-13, "手机号码已存在");
        /// <summary>
        /// 邮箱已存在
        /// </summary>
        public static KeyValuePair<int, string> Error_13 = new KeyValuePair<int, string>(-15, "邮箱已存在");
        /// <summary>
        /// 手机号码格式不正确
        /// </summary>
        public static KeyValuePair<int, string> Error_14 = new KeyValuePair<int, string>(-16, "手机号码格式不正确");
        /// <summary>
        /// 邮件格式不正确
        /// </summary>
        public static KeyValuePair<int, string> Error_15 = new KeyValuePair<int, string>(-17, "邮件格式不正确");
        /// <summary>
        /// 密码不能包含特殊符号
        /// </summary>
        public static KeyValuePair<int, string> Error_16 = new KeyValuePair<int, string>(-18, "密码不能包含特殊符号");
        /// <summary>
        /// 获取公钥失败
        /// </summary>
        public static KeyValuePair<int, string> Error_17 = new KeyValuePair<int, string>(-1, "获取公钥失败");
        /// <summary>
        /// 获取票据失败
        /// </summary>
        public static KeyValuePair<int, string> Error_18 = new KeyValuePair<int, string>(-1, "获取票据失败");
        /// <summary>
        /// 发送邮件失败
        /// </summary>
        public static KeyValuePair<int, string> Error_19 = new KeyValuePair<int, string>(-1, "发送邮件失败");
        /// <summary>
        /// 获取验证码失败
        /// </summary>
        public static KeyValuePair<int, string> Error_20 = new KeyValuePair<int, string>(-1, "获取验证码失败");
        /// <summary>
        /// 手机验证码错误
        /// </summary>
        public static KeyValuePair<int, string> Error_21 = new KeyValuePair<int, string>(-20, "手机验证码错误");
        /// <summary>
        /// 注册失败
        /// </summary>
        public static KeyValuePair<int, string> Error_22 = new KeyValuePair<int, string>(-1, "注册失败");
        /// <summary>
        /// 用户名已存在
        /// </summary>
        public static KeyValuePair<int, string> Error_23 = new KeyValuePair<int, string>(-14, "用户名已存在");
        /// <summary>
        /// token失效
        /// </summary>
        public static KeyValuePair<int, string> Error_24 = new KeyValuePair<int, string>(-11, "token失效");
        /// <summary>
        /// 修改会员信息失败
        /// </summary>
        public static KeyValuePair<int, string> Error_25 = new KeyValuePair<int, string>(-1, "修改会员信息失败");
        /// <summary>
        /// 修改密码失败
        /// </summary>
        public static KeyValuePair<int, string> Error_26 = new KeyValuePair<int, string>(-1, "修改密码失败");
        /// <summary>
        /// 上传头像参数错误
        /// </summary>
        public static KeyValuePair<int, string> Error_27 = new KeyValuePair<int, string>(-21, "上传头像参数错误");
        /// <summary>
        /// 上传失败
        /// </summary>
        public static KeyValuePair<int, string> Error_28 = new KeyValuePair<int, string>(-1, "上传失败");
        /// <summary>
        /// 修改手机号失败
        /// </summary>
        public static KeyValuePair<int, string> Error_29 = new KeyValuePair<int, string>(-1, "修改手机号失败");
        /// <summary>
        /// 获取手机号失败
        /// </summary>
        public static KeyValuePair<int, string> Error_30 = new KeyValuePair<int, string>(-1, "获取手机号失败");
        /// <summary>
        /// 修改昵称失败
        /// </summary>
        public static KeyValuePair<int, string> Error_31 = new KeyValuePair<int, string>(-1, "修改昵称失败");
        /// <summary>
        /// 用户不存在
        /// </summary>
        public static KeyValuePair<int, string> Error_32 = new KeyValuePair<int, string>(-19, "用户不存在");
        /// <summary>
        /// 收藏失败
        /// </summary>
        public static KeyValuePair<int, string> Error_33 = new KeyValuePair<int, string>(-1, "收藏失败");
        /// <summary>
        /// 获取列表失败
        /// </summary>
        public static KeyValuePair<int, string> Error_34 = new KeyValuePair<int, string>(-1, "获取列表失败");
        /// <summary>
        /// 取消收藏失败
        /// </summary>
        public static KeyValuePair<int, string> Error_35 = new KeyValuePair<int, string>(-1, "取消收藏失败");
        /// <summary>
        /// 支付密码错误
        /// </summary>
        public static KeyValuePair<int, string> Error_36 = new KeyValuePair<int, string>(-22, "支付密码错误");
        /// <summary>
        /// 设置支付密码失败
        /// </summary>
        public static KeyValuePair<int, string> Error_37 = new KeyValuePair<int, string>(-1, "设置支付密码失败");

        /// <summary>
        /// 账号密码错误
        /// </summary>
        public static KeyValuePair<int, string> Error_38 = new KeyValuePair<int, string>(-1, "账号密码错误");
        /// <summary>
        /// 修改积分失败
        /// </summary>
        public static KeyValuePair<int, string> Error_39 = new KeyValuePair<int, string>(-1, "修改积分失败");
        /// <summary>
        /// 登录名已存在
        /// </summary>
        public static KeyValuePair<int, string> Error_40 = new KeyValuePair<int, string>(-1, "登录名已存在");
        /// <summary>
        /// 未设置支付密码
        /// </summary>
        public static KeyValuePair<int, string> Error_41 = new KeyValuePair<int, string>(-1, "未设置支付密码");
        /// <summary>
        /// 密码不能为空
        /// </summary>
        public static KeyValuePair<int, string> Error_42 = new KeyValuePair<int, string>(-1, "密码不能为空");
        /// <summary>
        /// 财务账号不存在
        /// </summary>
        public static KeyValuePair<int, string> Error_43 = new KeyValuePair<int, string>(-1, "财务账号不存在");
        /// <summary>
        /// 当前用户未注册
        /// </summary>
        public static KeyValuePair<int, string> Error_44 = new KeyValuePair<int, string>(-1, "当前用户未注册");
        /// <summary>
        /// 当前手机号未注册
        /// </summary>
        public static KeyValuePair<int, string> Error_45 = new KeyValuePair<int, string>(-1, "当前手机号未注册");
        /// <summary>
        /// 当前邮箱未注册
        /// </summary>
        public static KeyValuePair<int, string> Error_46 = new KeyValuePair<int, string>(-1, "当前邮箱未注册");
        /// <summary>
        /// 账号格式无效
        /// </summary>
        public static KeyValuePair<int, string> Error_47 = new KeyValuePair<int, string>(-1, "账号格式无效");
        /// <summary>
        /// 登陆失败，稍后尝试
        /// </summary>
        public static KeyValuePair<int, string> Error_48 = new KeyValuePair<int, string>(-1, "登陆失败，稍后尝试");
        /// <summary>
        /// 修改密码过期超时，重新申请修改
        /// </summary>
        public static KeyValuePair<int, string> Error_49 = new KeyValuePair<int, string>(-1, "修改密码过期超时，重新申请修改");
        /// <summary>
        /// 请输入正确手机号码
        /// </summary>
        public static KeyValuePair<int, string> Error_50 = new KeyValuePair<int, string>(-1, "请输入正确手机号码");
        /// <summary>
        /// 激活用户超时，重新申请修改
        /// </summary>
        public static KeyValuePair<int, string> Error_51 = new KeyValuePair<int, string>(-1, "激活用户超时，重新申请修改");
        /// <summary>
        /// 发送邮件失败
        /// </summary>
        public static KeyValuePair<int, string> Error_52 = new KeyValuePair<int, string>(-1, "发送邮件失败");
        /// <summary>
        /// 发送短信失败
        /// </summary>
        public static KeyValuePair<int, string> Error_53 = new KeyValuePair<int, string>(-1, "发送短信失败");
        /// <summary>
        /// 获取会员信息失败
        /// </summary>
        public static KeyValuePair<int, string> Error_54 = new KeyValuePair<int, string>(-1, "获取会员信息失败");
        /// <summary>
        /// 支付失败
        /// </summary>
        public static KeyValuePair<int, string> Error_55 = new KeyValuePair<int, string>(-1, "支付失败");
        /// <summary>
        /// 退款失败
        /// </summary>
        public static KeyValuePair<int, string> Error_56 = new KeyValuePair<int, string>(-1, "退款失败");
        /// <summary>
        /// 充值失败
        /// </summary>
        public static KeyValuePair<int, string> Error_57 = new KeyValuePair<int, string>(-1, "充值失败");
        /// <summary>
        /// 更新头像失败
        /// </summary>
        public static KeyValuePair<int, string> Error_58 = new KeyValuePair<int, string>(-1, "更新头像失败");
        /// <summary>
        /// 账户余额不足
        /// </summary>
        public static KeyValuePair<int, string> Error_59 = new KeyValuePair<int, string>(-1, "账户余额不足");

        
        /// <summary>
        /// 系统错误
        /// </summary>
        public static KeyValuePair<int, string> Error_1000 = new KeyValuePair<int, string>(-1, "操作异常，请重试");
    }
}
