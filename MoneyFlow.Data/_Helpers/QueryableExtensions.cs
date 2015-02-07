using System;
using System.Linq;
using System.Linq.Expressions;

namespace MoneyFlow.Data
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Lookup<T>(this IQueryable<T> collection, Expression<Func<T, bool>> lookupPredicate)
        {
            return collection.Where(lookupPredicate);
        }
    }
}