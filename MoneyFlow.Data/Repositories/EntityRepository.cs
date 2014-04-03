using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MoneyFlow.Data.Contracts;

namespace MoneyFlow.Data
{
    public class EntityRepository<T> : IRepository<T>
        where T : class
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public EntityRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException();

            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<T> GetRange(int skipCount, int takeCount)
        {
            return DbSet.Skip(skipCount).Take(takeCount);
        }

        public virtual IQueryable<T> Lookup(Expression<Func<T, bool>> lookupPredicate)
        {
            return DbSet.Where(lookupPredicate);
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            var entry = DbContext.Entry(entity);

            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Added;

            else DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            var entry = DbContext.Entry(entity);

            if (entry.State != EntityState.Detached)
                DbSet.Attach(entity);
            
            else entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }
    }
}
