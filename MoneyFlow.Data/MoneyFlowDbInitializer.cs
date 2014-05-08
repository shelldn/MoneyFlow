using System.Data.Entity;

using SampleData = MoneyFlow.Seed.Data;

namespace MoneyFlow.Data
{
    public class MoneyFlowDbInitializer 
        : DropCreateDatabaseAlways<MoneyFlowDbContext>
    {
        protected override void Seed(MoneyFlowDbContext db)
        {
            SampleData.Seed(25, d => db.Categories.AddRange(d.Categories));

            db.SaveChanges();
        }
    }
}