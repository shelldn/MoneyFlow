using System;
using NUnit.Framework;

namespace MoneyFlow.Test.Repositories
{
    [TestFixture]
    public class CostsRepositoryTest
    {
        #region GetPeriods method spec.

        [Test, Category("GetPeriods")]
        public void Should_return_DateTime_collection()
        {
            throw new NotImplementedException();
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