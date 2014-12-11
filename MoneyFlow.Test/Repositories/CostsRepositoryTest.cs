using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MoneyFlow.Data;
using MoneyFlow.Model;
using MoneyFlow.Seed;
using Moq;
using NUnit.Framework;

namespace MoneyFlow.Test.Repositories
{
    [TestFixture]
    public partial class CostsRepositoryTest
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

        #region GetPeriods method spec.

        [Test, Category("GetPeriods")]
        public void Should_return_DateTime_collection()
        {
            // arrange
            
            var costs = new[]
            {
                new Cost { Date = DataProvider.GetDate("2014-01-01", "2014-12-31") },
                new Cost { Date = DataProvider.GetDate("2014-01-01", "2014-12-31") },
                new Cost { Date = DataProvider.GetDate("2014-01-01", "2014-12-31") }
            };

            Mock.Get(m_costs).ImplementQueryable(proto: costs);

            // act

            IEnumerable ret = m_repo.GetPeriods();

            // assert

            CollectionAssert.AllItemsAreInstancesOfType(ret, typeof(DateTime));
        }

        [Test, Category("GetPeriods")]
        public void Should_provide_all_the_values_with_1st_day_and_zero_time()
        {
            #region arrange

            List<Cost> costs = new List<Cost>();

            for (var i = 0; i < 10; i++)
            {
                costs.Add(new Cost { Date = DataProvider.GetDate("2014-01-01", "2014-12-31") });
            }

            Mock.Get(m_costs).ImplementQueryable(proto: costs);

            #endregion

            // act

            IEnumerable<DateTime> ret = m_repo.GetPeriods();

            // assert

            Assert.IsTrue(ret.All(p => p.Day == 1 && p.TimeOfDay == TimeSpan.Zero));
        }

        [Test, Category("GetPeriods")]
        public void Should_provide_the_values_distinct_by_month_and_year()
        {
            #region arrange

            var costs = new[]
            {
                // 2014-01
                DataProvider.GetDate("2014-01-01", "2014-01-31").Spend(),
                DataProvider.GetDate("2014-01-01", "2014-01-31").Spend(),
                DataProvider.GetDate("2014-01-01", "2014-01-31").Spend(),

                // 2014-07
                DataProvider.GetDate("2014-07-01", "2014-07-31").Spend(),
                DataProvider.GetDate("2014-07-01", "2014-07-31").Spend(),

                // 2014-11
                DataProvider.GetDate("2014-11-01", "2014-11-30").Spend(),
                DataProvider.GetDate("2014-11-01", "2014-11-30").Spend()
            };

            const int uc = 3;   // unique costs count

            Mock.Get(m_costs).ImplementQueryable(proto: costs);

            #endregion

            // act

            IEnumerable<DateTime> ret = m_repo.GetPeriods();

            // assert

            Assert.That(ret.Count(), Is.EqualTo(uc));
        }

        [Test, Category("GetPeriods")]
        [TestCaseSource(typeof(UnorderedCosts))]
        public void Should_provide_values_in_ascending_order(Cost[] costs)
        {
            #region arrange

            Mock.Get(m_costs).ImplementQueryable(proto: costs);

            #endregion

            // act

            IEnumerable<DateTime> ret = m_repo.GetPeriods();

            // assert

            Assert.That(ret, Is.Ordered);   // ascending order
        }

        [Test, Category("GetPeriods")]
        [TestCaseSource("GetCostsOfTheYear")]
        public void Should_provide_only_the_costly_periods(Cost[] costs)
        {
            #region arrange

            DateTime[] exp =
            {
                DateTime.Parse("2014-01"),
                DateTime.Parse("2014-03"),
                DateTime.Parse("2014-05"),
                DateTime.Parse("2014-12")
            };

            Mock.Get(m_costs).ImplementQueryable(proto: costs);

            #endregion

            // act

            IEnumerable<DateTime> ret = m_repo.GetPeriods();

            // assert

            CollectionAssert.AreEqual(ret, exp);
        }

        // Test:
        // 1. 1 month + 1 cost = 1 period.
        // 2. 1 month + M costs = 1 period.
        // 3. N months + 1 cost at month = N periods.
        // 4. N months + M costs at month = N periods.

        #endregion
    }
}