using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyFlow.Data;
using MoneyFlow.Data.Contracts;
using MoneyFlow.Model;

namespace MoneyFlow.Web.ApiControllers
{
    public class CostsController : ApiControllerBase
    {
        public CostsController(IMoneyFlowUow uow)
            : base(uow) { }

        public IQueryable<Cost> GetAll()
        {
            return Uow.Costs.GetAll();
        }
            
        [Route("api/consumptions/periods")]
        public IEnumerable<dynamic> GetPeriods()
        {
            return Uow.Costs.GetPeriods();
        }

        public IQueryable<Cost> GetByPeriod(DateTime period)
        {
            return Uow.Costs.GetByPeriod(period);
        }

        private static void CreateTimeStamp(Cost item)
        {
            item.Date = DateTime.Now;
        }

        public HttpResponseMessage PostConsumption(Cost item)
        {
            CreateTimeStamp(item);

            Uow.Costs.Add(item);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            var uri = Url.Link("DefaultApi", new { id = item.Id });
            
            response.Headers.Location = new Uri(uri);

            return response;
        }
    }
}