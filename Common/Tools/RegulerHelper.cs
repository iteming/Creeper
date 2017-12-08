using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Common.Tools
{
    public class RegulerHelper
    {
        public static RegulerHelper _RegulerHelper = new RegulerHelper();

        public bool IsMatch(string input, string pattern)
        {
            Regex reg = new Regex(pattern);

            return reg.IsMatch(input);
        }

        public string MatchResult(string input, string pattern)
        {
            Regex reg = new Regex(pattern);
            if (reg.IsMatch(input))
            {
                return reg.Match(input).Value;
            }
            return string.Empty;
        }

        /// <summary>
        /// 生成查询条件对象
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public Dictionary<string, string> CreateOptionDicByInput(string result)
        {
            string[] conditions = result.Split('-');

            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0, j = conditions.Length; i < j; i++)
            {
                var temp = conditions[i].Split('_');

                if (!dic.ContainsKey(temp[0]) && temp.Length == 2 && !string.IsNullOrEmpty(temp[0]) && !string.IsNullOrEmpty(temp[1]))
                {
                    dic.Add(temp[0], temp[1]);
                }
            }

            return dic;
        }
    }
}