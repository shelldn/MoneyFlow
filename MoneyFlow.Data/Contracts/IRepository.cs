using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MoneyFlow.Data
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(ObjectId id);
        Task CreateAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(ObjectId id);
    }
}