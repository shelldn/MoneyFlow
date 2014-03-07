using System.Collections.Generic;
using System.Linq;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Data
{
    public static class ConsumptionsRepository
    {
        public static IEnumerable<dynamic> GetPeriods(this IRepository<Consumption> repository)
        {
            return repository.GetAll()
                .Select(c => new { c.Date.Month, c.Date.Year })
                .Distinct();
        }
    }
}