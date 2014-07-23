using MoneyFlow.Model;

namespace MoneyFlow.Data.Contracts
{
    public interface IMoneyFlowUow
    {
        // save
        void Commit();

        // repos
        IRepository<Category> Categories { get; }
        IRepository<Cost> Costs { get; }
    }
}