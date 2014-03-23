using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoneyFlow.Model;

namespace MoneyFlow.Data.SampleData
{
    public class MoneyFlowDbInitializer 
        : DropCreateDatabaseAlways<MoneyFlowDbContext>
    {
        protected override void Seed(MoneyFlowDbContext context)
        {
            var categories = new List<Category>
            {
                new Category() { Description = "Продукты" },
                new Category() { Description = "Корм для собаки" },
                new Category() { Description = "Кино" }

            };

            var consumptions = new List<Consumption>
            {
                new Consumption() { Date = DateTime.Parse("2013-01-01T13:44:51"), Category = categories[0], Amount = 10.25m },
                new Consumption() { Date = DateTime.Parse("2013-01-25T20:51:21"), Category = categories[1], Amount = 125.50m },
                new Consumption() { Date = DateTime.Parse("2013-02-12T23:23:13"), Category = categories[0], Amount = 150m },
                new Consumption() { Date = DateTime.Parse("2013-02-15T01:11:15"), Category = categories[1], Amount = 235m },
                new Consumption() { Date = DateTime.Parse("2013-02-10T14:20:15"), Category = categories[2], Amount = 55.35m },
                new Consumption() { Date = DateTime.Parse("2014-01-02T17:25:21"), Category = categories[2], Amount = 100.25m },
                new Consumption() { Date = DateTime.Parse("2014-01-04T10:15:10"), Category = categories[1], Amount = 75.40m },
                new Consumption() { Date = DateTime.Parse("2014-01-06T09:55:01"), Category = categories[0], Amount = 88.50m }
            
            };

            context.Consumptions.AddRange(consumptions);
            context.SaveChanges();
        }
    }
}