using System;
using Common.Tools;

namespace Entity.Base
{
    public class ResultModel<T>
    {
        public int Ret { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public string Temp { get; set; }

        public override string ToString()
        {
            return ToolsHelper._ConvertTools.SerializeObject(this);
        }

        public string ToString(string format)
        {
            return ToolsHelper._ConvertTools.SerializeObject(this, format);
        }
    }

    public class ResultModelPager<T>
    {
        public int Ret { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int total { get; set; }

        public T rows { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int page { get; set; }
        
        /// <summary>
        /// 总条数
        /// </summary>
        public int records { get; set; }

        public override string ToString()
        {
            return ToolsHelper._ConvertTools.SerializeObject(this);
        }

        public string ToString(string format)
        {
            return ToolsHelper._ConvertTools.SerializeObject(this, format);
        }
    }
    
    public class ResultModel
    {
        /// <summary>
        /// 状态 0：失败 1：成功
        /// </summary>
        public int Ret { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }

        public override string ToString()
        {
            return ToolsHelper._ConvertTools.SerializeObject(this);
        }

        public string ToString(string format)
        {
            return ToolsHelper._ConvertTools.SerializeObject(this, format);
        }
    }

    public static class ResultMethod
    {
        /// <summary>
        /// 返回规则类（String）
        /// </summary>
        public static ResultModel SetResult(this string MsgStr, string ResultObj)
        {
            ResultModel result = new ResultModel();
            if (ResultObj == null)
                result.Ret = 0;
            else
                result.Ret = 1;
            result.Message = MsgStr;
            result.Data = ResultObj;
            return result;
        }
        
        /// <summary>
        /// 返回规则类
        /// </summary>
        public static ResultModel<T> SetResult<T>(this string MsgStr, T ResultObj)
        {
            ResultModel<T> result = new ResultModel<T>();
            if (ResultObj == null)
                result.Ret = 0;
            else
                result.Ret = 1;
            result.Message = MsgStr;
            result.Data = ResultObj;
            return result;
        }


        /// <summary>
        /// 返回规则类（带分页）
        /// </summary>
        public static ResultModelPager<T> SetResult<T>(this string MsgStr, T ResultObj, int Count)
        {
            ResultModelPager<T> result = new ResultModelPager<T>();
            if (ResultObj == null)
                result.Ret = 0;
            else
                result.Ret = 1;
            result.Message = MsgStr;
            result.rows = ResultObj;
            result.records = Count;
            return result;
        }

        /// <summary>
        /// 返回规则类（带分页）2
        /// </summary>
        public static ResultModelPager<T> SetResultPager<T>(this string MsgStr, T ResultObj, int Count = 0, int Page = 0, int Size = 0)
        {
            ResultModelPager<T> result = new ResultModelPager<T>();
            if (ResultObj == null)
                result.Ret = 0;
            else
            {
                result.Ret = 1;
                result.records = Count;
                result.total = Convert.ToInt32(Count % Size == 0 ? Count / Size : Count / Size + 1);
                result.page = Page;
            }
            result.Message = MsgStr;
            result.rows = ResultObj;
            return result;
        }
    }

}
