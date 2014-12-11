using System.Collections.Generic;
using System.Linq;

namespace MoneyFlow.Data
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Order<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(x => x);
        }
    }
}