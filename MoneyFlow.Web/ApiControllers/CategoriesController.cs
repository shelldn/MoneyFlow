using System.Linq;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Web.ApiControllers
{
    public class CategoriesController : ApiControllerBase
    {
        public CategoriesController(IMoneyFlowUow uow)
            : base(uow) { }

        public IQueryable<Category> GetAll()
        {
            return Uow.Categories.GetAll();
        }

        public IQueryable<Category> GetByLookupQuery(string q)
        {
            return Uow.Categories
                .Lookup(c => c.Words.Contains(q));
        }
    }
}