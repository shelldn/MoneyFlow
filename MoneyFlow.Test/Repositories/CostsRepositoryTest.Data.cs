using System.Collections;
using System.Collections.Generic;
using MoneyFlow.Model;
using MoneyFlow.Seed;
using NUnit.Framework;

namespace MoneyFlow.Test.Repositories
{
    public partial class CostsRepositoryTest
    {
        private class UnorderedCosts : IEnumerable<Cost[]>
        {
            public IEnumerator<Cost[]> GetEnumerator()
            {
                yield return new[]
                {
                    DataProvider.GetDate("2014-06-01", "2014-06-30").Spend(),
                    DataProvider.GetDate("2014-04-01", "2014-04-30").Spend(),
                    DataProvider.GetDate("2014-12-01", "2014-12-31").Spend()

                };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private static IEnumerable GetCostsOfTheYear()
        {
            yield return new[]
            {
                DataProvider.GetDate("2014-01-01", "2014-01-31").Spend(),
                DataProvider.GetDate("2014-03-01", "2014-03-31").Spend(),
                DataProvider.GetDate("2014-05-01", "2014-05-31").Spend(),
                DataProvider.GetDate("2014-12-01", "2014-12-31").Spend()
            };
        }
    }
}