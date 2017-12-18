using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    /// <summary>
    /// 记录日志帮助类
    /// </summary>
    public class LogHelper
    {
        private static readonly object obj = new object();//互斥锁

        /// <summary>
        /// 记录操作日志和异常日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <returns>记录成功返回true,否则返回false</returns>
        public void WriteToLog(string logContent)
        {
            lock (obj)
            {
                try
                {
                    //判断文件夹是否存在,不存在创建
                    if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "logFiles"))
                        Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "logFiles");

                    //得到文件名
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "logFiles\\logfile.txt";

                    if (File.Exists(fileName))
                    {
                        FileInfo fileInfo = new FileInfo(fileName);

                        //得到限制文件大小
                        int MaxSize = Convert.ToInt32(ConfigurationManager.AppSettings["logSize"]);

                        //如果文件大小超过限制，更改文件名称为备份名称
                        if (fileInfo.Length > 1024 * 1024 * MaxSize)
                        {
                            string desFileName = string.Format("{0}{1}{2}_log.txt.bak", AppDomain.CurrentDomain.BaseDirectory, "logFiles\\", DateTime.Now.ToString("yyyyMMddHHmmss"));

                            fileInfo.MoveTo(desFileName);
                        }
                    }

                    //生成日志文件
                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    string outputInfo = string.Format("{0}       {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), logContent);
                    sw.WriteLine(outputInfo);
                    sw.Flush();
                    sw.Close();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 得到异常日志
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="strSql"></param>
        /// <param name="exceptMessage"></param>
        /// <returns></returns>
        public string FormExceptionContent(string methodName, string strSql, string exceptMessage)
        {
            return string.Format("方法:{0}执行sql语句:{1}发生了异常:{2}", methodName, strSql, exceptMessage);
        }

        public static void WriteToLog(string logContent, string filePartName, bool showTime = true)
        {
            lock (obj)
            {
                try
                {
                    string strLogFilePath = ConfigurationManager.AppSettings["logFilePath"] + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                    if (!Directory.Exists(strLogFilePath))
                        Directory.CreateDirectory(strLogFilePath);
                    string fileName = strLogFilePath + "\\" + (string.IsNullOrWhiteSpace(filePartName) ? "___Log" : filePartName) + ".txt";

                    ////判断文件夹是否存在,不存在创建
                    //if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "logFiles"))
                    //    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "logFiles");

                    ////得到文件名
                    //string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\" + filePartName + ".txt";

                    //生成日志文件
                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    string outputInfo = string.Format("{0}  {1}", showTime ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") : "", logContent);
                    sw.WriteLine(outputInfo);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 写入长分割线
        /// </summary>
        /// <param name="filePartName"></param>
        public static void WriteToSplitLine(string filePartName, bool showTime = false)
        {
            WriteToLog("\r\n------------------------------------------------------------\r\n", filePartName, showTime);
        }
        /// <summary>
        /// 写入短分割线
        /// </summary>
        /// <param name="filePartName"></param>
        /// <param name="showTime"></param>
        public static void WriteToSplitLineShort(string filePartName, bool showTime = false)
        {
            WriteToLog("\r\n----------", filePartName, showTime);
        }

        /// <summary>
        /// 换行+空格
        /// </summary>
        public static string line_spaces = "\r\n                         ";

        public static void OutPutLogs(string logData, string directoryName)
        {
            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "/" + directoryName;
                //string strPath = HttpContext.Current.Server.MapPath(directoryName);//System.AppDomain.CurrentDomain.BaseDirectory + "/" + directoryName;
                string orderInfoPath = strPath + "/" + DateTime.Now.ToString("yyyy-MM-dd");
                if (!File.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                if (!File.Exists(orderInfoPath))
                {
                    Directory.CreateDirectory(orderInfoPath);
                }
                FileStream fs = new FileStream(orderInfoPath + "/" + DateTime.Now.ToString("yyyyMMddhh") + ".txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                //sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(logData);
                //sw.WriteLine("\r\n");
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch
            { }
        }

        public static T LogFuncRuntime<T>(string filePartName, string msg, Func<T> func)
        {
            if (func == null) throw new ArgumentNullException("func");

            Stopwatch timer = new Stopwatch();
            timer.Start();
            var result = func();
            timer.Stop();
            LogHelper.WriteToLog(string.Format("{0}:耗费{1}毫秒。 {2}", msg,
                timer.ElapsedMilliseconds, timer.ElapsedMilliseconds > 500 ? "超过性能要求标准。" : ""), filePartName);
            return result;
        }
    }
}
