using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MoneyFlow.Data;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Web.ApiControllers
{
    public class CostsController : ApiControllerBase
    {
        public CostsController(IMoneyFlowUow uow)
            : base(uow) { }

        public IQueryable<Cost> GetAll()
        {
            return Uow.Costs.GetAll();
        }
            
        [Route("api/costs/periods")]
        public IEnumerable<dynamic> GetPeriods()
        {
            return Uow.Costs.GetPeriods();
        }

        public IQueryable<Cost> GetByPeriod(DateTime period)
        {
            return Uow.Costs.GetByPeriod(period);
        }

        private static void CreateTimeStamp(Cost model)
        {
            model.Date = DateTime.Now;
        }

        //
        // POST: /api/costs

        public IHttpActionResult Post(Cost model)
        {
            CreateTimeStamp(model);

            Uow.Costs.Add(model);
            Uow.Commit();

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }
    }
}