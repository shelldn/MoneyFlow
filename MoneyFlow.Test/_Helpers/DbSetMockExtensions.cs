using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace MoneyFlow.Test
{
    internal static class DbSetMockExtensions
    {
        public static void ImplementQueryable<T>(this Mock<DbSet<T>> mock, IEnumerable<T> proto) where T : class
        {
            var mq = mock.As<IQueryable<T>>();
            var q = proto.AsQueryable();

            mq.Setup(s => s.Provider).Returns(q.Provider);
            mq.Setup(s => s.Expression).Returns(q.Expression);
            mq.Setup(s => s.ElementType).Returns(q.ElementType);

            // enumerator

            mq.Setup(s => s.GetEnumerator()).Returns(q.GetEnumerator);
        }
    }
}