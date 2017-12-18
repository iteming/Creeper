using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.WxShare.Lib;

namespace Common.WxShare
{
    public class JsApiShare
    {
        public static string GetShareSignature(string noncestr, string timestamp)
        {
            return ShareUtil.Getsignature(noncestr, timestamp);
        }
    }
}
