using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Data
{
    #region Extensions

    public static class CostsRepositoryExtensions
    {
        public static IEnumerable<DateTime> GetPeriods(this IRepository<Cost> repo)
        {
            // group and select key
            var raw = repo.GetAll()
                .GroupBy(c => new { c.Date.Year, c.Date.Month })
                .Select(x => x.Key)

                .ToList();  // materialize

            // cast to date and order
            // by ascending
            return raw
                .Select(x => new DateTime(x.Year, x.Month, 1))
                .Order();
        }
    }

    #endregion

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
    }
}