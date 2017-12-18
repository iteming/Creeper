using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Base;

namespace Entity.Dto
{
    public class DtoRebateStatistics
    {
        public User user { get; set; }
        public Agent agent { get; set; }
        public decimal? CanRebateAmount { get; set; }

        public decimal? TotalRebateAmount { get; set; }
        public decimal? TodayRebateAmount { get; set; }
        public decimal? YestodayRebateAmount { get; set; }
        public decimal? WeekRebateAmount { get; set; }
        public decimal? MonthRebateAmount { get; set; }


        public int TotalUserCount { get; set; }
        public int TodayUserCount { get; set; }
        public int YestodayUserCount { get; set; }
        public int WeekUserCount { get; set; }
        public int MonthUserCount { get; set; }
    }
}
