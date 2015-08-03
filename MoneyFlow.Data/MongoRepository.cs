using System.Collections.Generic;
using System.Threading.Tasks;
using Humanizer;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MoneyFlow.Data
{
    public sealed class MongoRepository<T> : IRepository<T>
    {
        private readonly IMongoCollection<T> _set;

        public MongoRepository(/*IMongoDatabase db*/)
        {
            var db = new MongoClient("mongodb://localhost:27017").GetDatabase("mf");

            var collectionName = typeof(T).Name
                .Pluralize()
                .Camelize();

            _set = db.GetCollection<T>(collectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _set.Find(all => true).ToListAsync();

        public Task<T> GetAsync(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateAsync(T obj)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(T obj)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(ObjectId id)
        {
            throw new System.NotImplementedException();
        }
    }
}