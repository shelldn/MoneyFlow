using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoneyFlow.Model;

namespace MoneyFlow.Data
{
    public class CostsRepository : EntityRepository<Cost>
    {
        public CostsRepository(DbContext dbContext)
            : base(dbContext) { }

        public override void Add(Cost entity)
        {
            DbSet.Add(entity);

            // Если категория уже есть в б.д.
            // то помечаем ее как неизмененную
            if (entity.Category.Id > 0)
                DbContext.Entry(entity.Category).State = 
                    EntityState.Unchanged;
        }

        public IEnumerable<DateTime> GetPeriods()
        {
            return DbSet
                .GroupBy(c => new DateTime(c.Date.Year, c.Date.Month, 1))
                .Select(c => c.Key)
                .OrderBy(c => c);
        }
    }
}