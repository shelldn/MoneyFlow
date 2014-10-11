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