using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class DtoUserGame
    {
        public int UserId { get; set; }
        public string NickName { get; set; }
        public decimal? TotalRound { get; set; } //总局数
        public decimal? TodayRound { get; set; } //今日局数
        public decimal? TotalWinRound { get; set; } //总赢得局数
        public decimal? TodayWinRound { get; set; } //今日赢得局数
        public decimal? Diamond { get; set; } // 开房消耗钻石
    }
}
