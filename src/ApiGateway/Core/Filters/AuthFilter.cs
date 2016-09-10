using System.Linq;
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
            var request = context.Request;
            var accessKey = request.Form["AccessKey"].SingleOrDefault();
            if (!_provider.Authenticate(context.RequestModel.ApiName, accessKey))
            {
                throw new ApiGatewayException(401, "Not Allowed");
            }
        }
    }
}