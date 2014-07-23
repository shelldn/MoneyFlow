using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Data
{
    public static class ConsumptionsRepository
    {
        public static IQueryable<Cost> GetAllConsumptions(this IRepository<Cost> repository)
        {
            return repository.GetAll().Include(c => c.Category);
        }

        public static IEnumerable<dynamic> GetPeriods(this IRepository<Cost> repository)
        {
            return repository.GetAllConsumptions()
                .Select(c => new { c.Date.Month, c.Date.Year })
                .Distinct();
        }

        public static IQueryable<Cost> GetByPeriod(this IRepository<Cost> repository, DateTime period)
        {
            return repository.GetAllConsumptions()
                .Where(c => c.Date.Month == period.Month && c.Date.Year == period.Year);

        }
    }
}