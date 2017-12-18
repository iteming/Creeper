using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace Common.Tools
{
    /// <summary>
    /// 类型转化
    /// </summary>
    public class ConvertTools
    {
        public static ConvertTools _ConvertTools = new ConvertTools();

        public string SerializeObject(object value, string format = "yyyy-MM-dd")
        {
            JsonConverter[] converters = new JsonConverter[] { new IsoDateTimeConverter { DateTimeFormat = format } };
            return JsonConvert.SerializeObject(value, converters);
        }

        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public decimal StringToDecimal(string input, decimal defaultVal = 0)
        {
            decimal.TryParse(input, out defaultVal);
            return defaultVal;
        }
        public int StringToInt(string input, int defaultVal = 0)
        {
            int.TryParse(input, out defaultVal);
            return defaultVal;
        }
        public long StringToLong(string input, long defaultVal = 0)
        {
            long.TryParse(input, out defaultVal);
            return defaultVal;
        }
        public DateTime? StringToDateTime(string input)
        {
            if(String.IsNullOrEmpty(input))
                return null;
            DateTime defaultVal = new DateTime(1991, 1, 1);
            DateTime.TryParse(input, out defaultVal);
            return defaultVal;
        }
        public byte StringToByte(string input, byte defaultVal = 0)
        {   
            byte.TryParse(input, out defaultVal);
            return defaultVal;
        }
        public double StringToDouble(string input, double defaultVal = 0)
        {
            double.TryParse(input, out defaultVal);
            return defaultVal;
        }

        /// <summary>
        /// 获取参数对象（来源json）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">json字符串</param>
        /// <returns></returns>
        public T JsonToObject<T>(string jsonString) where T : new()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch
            {
                //参数异常，做记录（返回空）
            }
            return new T();
        }

        /// <summary>
        /// 将Stream 转化为 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }


        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }
    }
}
