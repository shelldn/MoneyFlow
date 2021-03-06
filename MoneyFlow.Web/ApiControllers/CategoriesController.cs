﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public HttpResponseMessage PostCategory(Category item)
        {
            Uow.Categories.Add(item);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            var uri = Url.Link("DefaultApi", new { id = item.Id });

            response.Headers.Location = new Uri(uri);

            return response;
        }
    }
}