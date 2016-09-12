using ApiGateway.Core.Contexts;
using ApiGateway.Core.Exceptions;
using ApiGateway.Core.Filters;
using ApiGateway.Core.IoC;
using ApiGateway.Core.Providers;

namespace ApiGateway.Filters
{
    public class AuthFilter : IFilter
    {
        private readonly IAuthenticationProvider _provider = ObjectContainer.Resolve<IAuthenticationProvider>();

        public FilterType FilterType => FilterType.Pre;
        public int FilterOrder => 10;

        public void Execute()
        {
            var context = RequestContext.Current;

            var apiName = context.RequestModel.ApiName;
            var accessKey = context.RequestModel.AccessKey;
            if (!_provider.Authenticate(apiName, accessKey))
            {
                throw new ApiGatewayException(401, "Not Allowed");
            }
        }
    }
}