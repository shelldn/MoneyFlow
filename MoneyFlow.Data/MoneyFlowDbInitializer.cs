using System;
using System.Data.Entity;
using MoneyFlow.Model;
using MoneyFlow.Seed;

using SampleData = MoneyFlow.Seed.Data;

namespace MoneyFlow.Data
{
    public class MoneyFlowDbInitializer 
        : IDatabaseInitializer<MoneyFlowDbContext>
    {
        public void InitializeDatabase(MoneyFlowDbContext context)
        {
            throw new NotImplementedException();
        }
    }
}