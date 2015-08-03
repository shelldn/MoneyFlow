using MoneyFlow.Data;
using MoneyFlow.Data.Models;
using Nancy;

namespace MoneyFlow.Api.Modules
{
    public class IncomesModule : NancyModule
    {
        private readonly IRepository<Income> _incomes;

        public IncomesModule(IRepository<Income> incomes) : base("/incomes")
        {
            _incomes = incomes;

            Get["/", true] = async (x, ct) => await _incomes.GetAllAsync();
        }
    }
}