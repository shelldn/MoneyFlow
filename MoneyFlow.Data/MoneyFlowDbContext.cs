using System.Data.Entity;
using MoneyFlow.Data.SampleData;
using MoneyFlow.Model;

namespace MoneyFlow.Data
{
    public class MoneyFlowDbContext : DbContext
    {
        public MoneyFlowDbContext()
            : base("name=DefaultConnection")
        {
            Database.SetInitializer(new MoneyFlowDbInitializer());
        }

        public DbSet<Category> Categories{ get; set; }
        public DbSet<Consumption> Consumptions { get; set; }
    }
}