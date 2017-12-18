using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class DtoUserRanking
    {
        public int Rank { get; set; }
        public int UserId { get; set; }
        public string NickName { get; set; }
        public decimal ChargeAmount { get; set; }
        public DateTime Writedate { get; set; }
    }
}
