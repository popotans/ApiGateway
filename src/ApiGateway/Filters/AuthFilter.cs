using ApiGateway.Core.Contexts;
using ApiGateway.Core.Exceptions;
using ApiGateway.Core.Filters;
using ApiGateway.Core.IoC;
using ApiGateway.Core.Providers;
using System.Linq;

namespace ApiGateway.Filters
{
    public class AuthFilter : IFilter
    {
        private readonly IAuthenticationProvider _provider = ObjectContainer.Resolve<IAuthenticationProvider>();

        public FilterType FilterType => FilterType.Pre;
        public int FilterOrder => 1;

        public void Execute()
        {
            var context = RequestContext.Current;

            var accessKey = context.Request.Form["AccessKey"].SingleOrDefault();
            if (!_provider.Authenticate(context.RequestModel.ApiName, accessKey))
            {
                throw new ApiGatewayException(401, "Not Allowed");
            }
        }
    }
}