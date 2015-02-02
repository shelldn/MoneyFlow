using System.Web.Http;
using System.Web.Http.Controllers;
using MoneyFlow.Model;

namespace MoneyFlow.Web.ModelBinding
{
    public class PersonalAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor param)
        {
            var isAssignable = typeof(IPersonal)
                .IsAssignableFrom(param.ParameterType);

            return isAssignable ? 
                PersonalParameterBinding.CreateDefault(param) : 
                param.BindAsError("Unsupported type");
        }
    }
}