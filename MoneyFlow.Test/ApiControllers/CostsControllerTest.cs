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
        #region Test maintenance



        #endregion

        [Test, Category("GetByPeriod(DateTime)")]
        public void Should_return_only_the_costs_within_specified_month_and_year()
        {
            #region arrange

            var uow = Mock.Of<IMoneyFlowUow>();
            var ctrl = new CostsController(uow);

            var period = new DateTime(2014, 05, 01);

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

            Mock.Get(uow)
                .Setup(u => u.Costs.GetAll())
                .Returns(costs.AsQueryable());

            #endregion

            // act

            var ret = ctrl.GetByPeriod(period);

            // assert

            Assert.That(ret, Is.EquivalentTo(exp));
        }
    }
}