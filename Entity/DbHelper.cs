using System.Data.Entity;
using Entity.Base;

namespace Entity
{
    public class DbHelper : DbContext
    {
        public DbHelper() : base("CreeperEntities") { }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<AgentLevel> AgentLevel { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Charge> Charge { get; set; }
        public DbSet<AgentApply> AgentApply { get; set; }
    }
}
