using System;
using ApiGateway.Core.Contexts;
using ApiGateway.Core.Exceptions;
using ApiGateway.Core.Filters;
using ApiGateway.Core.IoC;
using ApiGateway.Core.Providers;

namespace ApiGateway.Filters
{
    public class ParameterVerificationFilter : IFilter
    {
        private readonly IAuthenticationProvider _authenticationProvider = ObjectContainer.Resolve<IAuthenticationProvider>();

        public FilterType FilterType => FilterType.Pre;
        public int FilterOrder => 1;

        public void Execute()
        {
            var request = RequestContext.Current.RequestModel;

            if (string.IsNullOrWhiteSpace(request.AccessKey))
                throw new ApiGatewayException(400, "AccessKey is required");

            if (request.Timestamp == DateTime.MinValue)
                throw new ApiGatewayException(400, "Invalid Timestamp");

            if (string.IsNullOrWhiteSpace(request.Sign))
                throw new ApiGatewayException(400, "Sign is required");

            TimeSpan timeSpan = DateTime.Now - request.Timestamp;
            if (Math.Abs(timeSpan.TotalMinutes) > 10)
                throw new ApiGatewayException(400, "Request expired");

            if (!_authenticationProvider.CheckSign(request.AccessKey, request.Timestamp, request.Body, request.Sign))
                throw new ApiGatewayException(400, "Invalid Sign");
        }
    }
}
