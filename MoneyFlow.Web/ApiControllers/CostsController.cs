using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return Uow.Costs.GetAll()
                .Include(c => c.Category);
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