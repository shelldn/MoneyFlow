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
    public class ConsumptionsController : ApiControllerBase
    {
        public ConsumptionsController(IMoneyFlowUow uow)
            : base(uow) { }

        public IQueryable<Consumption> GetAll()
        {
            return Uow.Consumptions.GetAll();
        }
            
        [Route("api/consumptions/periods")]
        public IEnumerable<dynamic> GetPeriods()
        {
            return Uow.Consumptions.GetPeriods();
        }

        public IQueryable<Consumption> GetByPeriod(DateTime period)
        {
            return Uow.Consumptions.GetByPeriod(period);
        }

        private static void CreateTimeStamp(Consumption item)
        {
            item.Date = DateTime.Now;
        }

        public HttpResponseMessage PostConsumption(Consumption item)
        {
            CreateTimeStamp(item);

            Uow.Consumptions.Add(item);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            var uri = Url.Link("DefaultApi", new { id = item.Id });
            
            response.Headers.Location = new Uri(uri);

            return response;
        }
    }
}