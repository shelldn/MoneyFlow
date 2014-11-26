using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using MoneyFlow.Data;
using MoneyFlow.Model;
using Moq;
using NUnit.Framework;

namespace MoneyFlow.Test.Repositories
{
    [TestFixture]
    public class CostsRepositoryTest
    {
        private DbSet<Cost> m_costs;
        private DbContext m_db;
        private CostsRepository m_repo;

        #region Test maintenance

        [SetUp]
        public void Init()
        {
            m_costs = Mock.Of<DbSet<Cost>>();
            m_db = Mock.Of<DbContext>();

            // setup

            Mock.Get(m_db)
                .Setup(db => db.Set<Cost>())
                .Returns(m_costs);

            // repo

            m_repo = new CostsRepository(m_db);
        }

        [TearDown]
        public void Clean()
        {
            m_costs = null;
            m_db = null;
            m_repo = null;
        }

        #endregion

        #region Helpers

        private static void ImplementQueryable<T>(Mock<IQueryable<T>> mock, IQueryable<T> q)
        {
            mock.Setup(c => c.Provider).Returns(q.Provider);
            mock.Setup(c => c.Expression).Returns(q.Expression);
            mock.Setup(c => c.ElementType).Returns(q.ElementType);

            // enumerator

            mock.Setup(c => c.GetEnumerator()).Returns(q.GetEnumerator);
        }

        #endregion

        #region GetPeriods method spec.

        [Test, Category("GetPeriods")]
        public void Should_return_DateTime_collection()
        {
            // arrange

            var q = new[]
            {
                new Cost { Date = DateTime.Parse("2014-01-14T13:24:07.123") },
                new Cost { Date = DateTime.Parse("2014-02-06T23:17:15.425") },
                new Cost { Date = DateTime.Parse("2014-02-23T05:10:45.999") }

            }.AsQueryable();

            var costsMock = Mock
                .Get(m_costs).As<IQueryable<Cost>>();
            
            ImplementQueryable(costsMock, q);

            // act

            IEnumerable ret = m_repo.GetPeriods();

            // assert

            CollectionAssert.AllItemsAreInstancesOfType(ret, typeof(DateTime));
        }

        [Test, Category("GetPeriods")]
        public void Should_provide_all_the_values_with_1st_day_and_zero_time()
        {
            throw new NotImplementedException();
        }

        [Test, Category("GetPeriods")]
        public void Should_provide_the_values_distinct_by_month_and_year()
        {
            throw new NotImplementedException();
        }

        [Test, Category("GetPeriods")]
        public void Should_provide_values_in_ascending_order()
        {
            throw new NotImplementedException();
        }

        [Test, Category("GetPeriods")]
        public void Should_provide_only_the_costly_periods()
        {
            throw new NotImplementedException();
        }

        [TestCase]  // 1. 1 month + 1 cost = 1 period.
        [TestCase]  // 2. 1 month + M costs = 1 period.
        [TestCase]  // 3. N months + 1 cost at month = N periods.
        [TestCase]  // 4. N months + M costs at month = N periods.

        [Category("GetPeriods")]
        public void Should_translate_costs_to_corresponding_periods()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}