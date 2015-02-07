using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Web.ApiControllers
{
    [RoutePrefix("api/costs")]
    public class CostsController : ApiControllerBase
    {
        public CostsController(IMoneyFlowUow uow)
            : base(uow) { }

        [Route("periods")]
        public IEnumerable<DateTime> GetPeriods()
        {
            var dates = GetAll().Select(c => c.Date);
            var p = dates.Min().GetMonth();

            while (p <= dates.Max())
            {
                yield return p;
                p = p.AddMonths(1);
            }
        }

        public IQueryable<Cost> GetAll()
        {
            return Uow.Costs.GetAll()
                .Include(c => c.Category);
        }

        //
        // GET: /api/costs/2014-05

        [Route("{period:DateTime}")]
        public IQueryable<Cost> GetByPeriod(DateTime period)
        {
            return GetAll()
                .Where(c => c.Date.Month == period.Month && c.Date.Year == period.Year);
        }

        //
        // POST: /api/costs

        public IHttpActionResult Post(Cost model)
        {
            Uow.Costs.Add(model);
            Uow.Commit();

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }
    }
}