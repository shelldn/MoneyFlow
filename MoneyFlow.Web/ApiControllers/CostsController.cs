using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
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
            var accountId = User.Identity.GetUserId<int>();

            return Uow.Costs.GetAll()
                .Include(c => c.Category)
                .Where(c => c.AccountId == accountId);
        }

        //
        // POST: /api/costs

        public IHttpActionResult Post(Cost model)
        {
            var accountId = User.Identity.GetUserId<int>();

            model.AccountId = model.Category.AccountId = accountId;

            Uow.Costs.Add(model);
            Uow.Commit();

            return CreatedAtRoute("DefaultApi", new { id = model.Id }, model);
        }
    }
}