using System;
using System.Data.Entity;
using MoneyFlow.Seed;

using SampleData = MoneyFlow.Seed.Data;

namespace MoneyFlow.Data
{
    public class MoneyFlowDbInitializer 
        : DropCreateDatabaseAlways<MoneyFlowDbContext>
    {
        public SeedOptions Options { get; private set; }

        public MoneyFlowDbInitializer()
        {
            Options = new SeedOptions
            {
                ConsumptionCount = 25,
                MinDate = new DateTime(2014, 1, 1),
                MaxDate = new DateTime(2014, 12, 31),
                MinAmount = 1,
                MaxAmount = 500
            };
        }

        protected override void Seed(MoneyFlowDbContext db)
        {
            SampleData.Seed(Options, d => db.Consumptions.AddRange(d.Consumptions));

            db.SaveChanges();
        }
    }
}