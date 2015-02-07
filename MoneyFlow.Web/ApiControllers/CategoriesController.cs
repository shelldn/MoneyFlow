using System.Linq;
using MoneyFlow.Data;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Web.ApiControllers
{
    public class CategoriesController : ApiControllerBase
    {
        public CategoriesController(IMoneyFlowUow uow)
            : base(uow) { }

        public IQueryable<Category> GetPersonal()
        {
            var accountId = User.GetId();

            return Uow.Categories.GetAll()
                .Where(c => c.AccountId == accountId);
        }

        public IQueryable<Category> GetByLookupQuery(string q)
        {
            return GetPersonal()
                .Lookup(c => c.Words.Contains(q));
        }
    }
}