﻿using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Data
{
    public class MoneyFlowUow : IMoneyFlowUow
    {
        protected readonly MoneyFlowDbContext DbContext
            = new MoneyFlowDbContext();

        public IRepository<Category> Categories{ get { return CreateRepository<Category>(); } }
        public IRepository<Cost> Costs { get { return new CostsRepository(DbContext); } }

        // factory method
        public IRepository<T> CreateRepository<T>() where T : class
        {
            return new EntityRepository<T>(DbContext);
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}