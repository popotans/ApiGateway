using ApiGateway.NET46.Core.Contexts;
using ApiGateway.NET46.Core.Exceptions;
using ApiGateway.NET46.Core.IoC;
using ApiGateway.NET46.Core.Providers;

namespace ApiGateway.NET46.Core.Filters
{
    public class AuthFilter : IFilter
    {
        private readonly IAuthenticationProvider _provider = ObjectContainer.Resolve<IAuthenticationProvider>();

        public FilterType FilterType => FilterType.Pre;
        public int FilterOrder => 1;

        public void Execute()
        {
            var context = ApiGatewayContext.Current;

            var accessKey = context.Request.Form["AccessKey"];
            if (!_provider.Authenticate(context.ApiName, accessKey))
            {
                throw new ApiGatewayException(401, "Not Allowed");
            }
        }
    }
}