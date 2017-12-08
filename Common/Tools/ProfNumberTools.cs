using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creeper.Tools
{
    public class ProfNumberTools
    {
        /// <summary>
        /// 获取专业类别前缀
        /// </summary>
        /// <param name="ProfNumber">类别编号</param>
        public string ConvertProfNumber(string ProfNumber)
        {
            if (ProfNumber.Length != 6)
                throw new Exception("编码必须为六位数字！");
            if (!ProfNumber.Substring(4, 2).Equals("00"))
                throw new Exception("无子类别信息！");
            else
                ProfNumber = ProfNumber.Substring(0, 4);
            if (ProfNumber.Substring(2, 2).Equals("00"))
                ProfNumber = ProfNumber.Substring(0, 2);
            return ProfNumber;
        }
    }
}
