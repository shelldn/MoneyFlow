using System;
using System.Linq;
using System.Linq.Expressions;

namespace MoneyFlow.Data.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetRange(int skipCount, int takeCount);
        IQueryable<T> Lookup(Expression<Func<T, bool>> lookupPredicate);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}