﻿using System.Web.Http;
using MoneyFlow.Data.Contracts;

namespace MoneyFlow.Web.ApiControllers
{
    public class ApiControllerBase : ApiController
    {
        public IMoneyFlowUow Uow { get; set; }

        public ApiControllerBase(IMoneyFlowUow uow)
        {
            Uow = uow;
        }
    }
}