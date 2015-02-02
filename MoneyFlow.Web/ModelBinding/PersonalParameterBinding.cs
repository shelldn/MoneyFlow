using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using Microsoft.AspNet.Identity;
using MoneyFlow.Model;

namespace MoneyFlow.Web.ModelBinding
{
    public class PersonalParameterBinding : FormatterParameterBinding
    {
        public PersonalParameterBinding(HttpParameterDescriptor descriptor, IEnumerable<MediaTypeFormatter> formatters, IBodyModelValidator validator) 
            : base(descriptor, formatters, validator) { }

        private static int GetAccountId(HttpActionContext actionContext)
        {
            return actionContext.RequestContext.Principal.Identity.GetUserId<int>();
        }

        public override async Task ExecuteBindingAsync(
            ModelMetadataProvider metadataProvider, 
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            IPersonal personal;

            await base.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken);

            personal = (IPersonal)GetValue(actionContext);
            personal.AccountId = GetAccountId(actionContext);
        }

        public static HttpParameterBinding CreateDefault(HttpParameterDescriptor param)
        {
            var config = param.Configuration;
            var formatters = config.Formatters;
            var validator = config.Services.GetService(typeof(IBodyModelValidator)) as IBodyModelValidator;

            return new PersonalParameterBinding(param, formatters, validator);
        }
    }
}