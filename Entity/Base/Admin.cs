using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Base
{
    public class Admin
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
