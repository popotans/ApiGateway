using ApiGateway.Core.Contexts;
using ApiGateway.Core.Exceptions;
using ApiGateway.Core.Providers;

namespace ApiGateway.Core.Filters
{
    public class AuthFilter : IFilter
    {
        private IAuthenticationProvider _provider;

        public FilterType FilterType => FilterType.Pre;
        public int FilterOrder => 1;

        public void Execute()
        {
            var context = RequestContext.Current;
            if (!_provider.Authenticate(context.ApiName, context.AccessKey))
            {
                throw new ApiGatewayException(401, "Not Allowed");
            }
        }
    }
}