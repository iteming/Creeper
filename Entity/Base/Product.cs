using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Base
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int IsValid { get; set; } // 是否有效
        public string Remark { get; set; }
        public string ServerApiUrl { get; set; }
        public string DBWanIP { get; set; }
        public string DBIntranetIP { get; set; }
        public string DBProt { get; set; }
    }
}
