using System;
using System.Linq;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;
using MoneyFlow.Web.ApiControllers;
using Moq;
using NUnit.Framework;

namespace MoneyFlow.Test.ApiControllers
{
    [TestFixture]
    public class CostsControllerTest
    {
        #region test_maintenance

        private CostsController m_ctrl;
        private IMoneyFlowUow m_uow;

        [SetUp]
        public void Init()
        {
            m_uow = Mock.Of<IMoneyFlowUow>();
            m_ctrl = new CostsController(m_uow);
        }

        #endregion

        [Test, Category("GetPeriods()")]
        public void Should_return_the_DateTime_collection()
        {
            #region arrange

            Cost[] costs = { Cost.At("2015-07-10T14:00:00") };

            Mock.Get(m_uow)
                .Setup(u => u.Costs.GetAll())
                .Returns(costs.AsQueryable());

            #endregion

            // act

            var periods = m_ctrl.GetPeriods();

            // assert

            Assert.That(periods, Is.All.InstanceOf<DateTime>());
        }

        [Test, Category("GetPeriods()")]
        public void Should_return_only_unique_periods()
        {
            #region arrange

            var months2015 = Enumerable.Range(1, 12);
            var twoDays = new[] { 10, 20 };

            var costs2015 = months2015
                .SelectMany(m => twoDays
                    .Select(d => Cost.At(2015, m, d)));

            Mock.Get(m_uow)
                .Setup(u => u.Costs.GetAll())
                .Returns(costs2015.AsQueryable());

            #endregion

            // act

            var periods = m_ctrl.GetPeriods();

            // assert

            Assert.That(periods, Is.Unique);
        }

        [Test, Category("GetPeriods()")]
        public void Should_return_the_dates_with_1st_day_and_zero_time()
        {
            #region arrange

            Cost[] costs =
            {
                Cost.At("2014-06-10T14:00:00"),
                Cost.At("2014-07-10T14:00:00")
            };

            Mock.Get(m_uow)
                .Setup(u => u.Costs.GetAll())
                .Returns(costs.AsQueryable());

            #endregion

            // act

            var periods = m_ctrl.GetPeriods();

            // assert

            Predicate<DateTime> monthStart = p =>
                p.TimeOfDay == new TimeSpan(00, 00, 00) &&
                p.Day == 01;

            Assert.That(periods, Is.All.Matches(monthStart));
        }

        [Test, Category("GetPeriods()")]
        public void Should_return_all_the_periods_starting_from_1st_and_ending_with_last_cost()
        {
            #region arrange
            
            var months2015 = Enumerable.Range(1, 12)
                .Select(i => new DateTime(2015, i, 1))
                .ToArray();

            var firstPeriod = months2015.First();
            var lastPeriod = months2015.Last();

            Cost[] costs =
            {
                Cost.At(firstPeriod.AddDays(10)),
                Cost.At(lastPeriod.AddDays(10))
            };

            Mock.Get(m_uow)
                .Setup(u => u.Costs.GetAll())
                .Returns(costs.AsQueryable());

            #endregion

            // act

            var periods = m_ctrl.GetPeriods();

            // assert

            Assert.That(periods, Is.EquivalentTo(months2015));
        }

        [Test, Category("GetPeriods()")]
        public void Should_return_all_the_periods_in_ascending_order()
        {
            #region arrange

            int[] months = { 3, 1, 6, 7, 12, 7 };     // unordered months

            var costs = months
                .Select(m => Cost.At(2015, m, 10));

            Mock.Get(m_uow)
                .Setup(u => u.Costs.GetAll())
                .Returns(costs.AsQueryable());

            #endregion

            // act

            var periods = m_ctrl.GetPeriods().ToArray();
            var firstMonth = periods.First().Month;
            var lastMonth = periods.Last().Month;

            // assert

            Assert.That(periods, Is.Ordered);
            Assert.That(firstMonth, Is.EqualTo(1));
            Assert.That(lastMonth, Is.EqualTo(12));
        }

        [Test, Category("GetByPeriod(DateTime)")]
        public void Should_return_only_the_costs_within_specified_month_and_year()
        {
            #region arrange

            var period = new DateTime(2014, 07, 01);

            Cost[] exp =
            {
                Cost.At(period),
                Cost.At(period.AddDays(10)),
                Cost.At(period.AddDays(20))
            };

            var costs = exp // all costs including expected

                // before
                .Concat(new[]
                {
                    Cost.At(period.AddDays(-10)),
                    Cost.At(period.AddMonths(-10)),
                    Cost.At(period.AddYears(-10))
                })

                // after
                .Concat(new[]
                {
                    Cost.At(period.AddMonths(10)),
                    Cost.At(period.AddYears(10))
                });

            Mock.Get(m_uow)
                .Setup(u => u.Costs.GetAll())
                .Returns(costs.AsQueryable());

            #endregion

            // act

            var ret = m_ctrl.GetByPeriod(period);

            // assert

            Assert.That(ret, Is.EquivalentTo(exp));
        }
    }
}