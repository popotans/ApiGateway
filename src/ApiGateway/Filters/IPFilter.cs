using ApiGateway.Core.Contexts;
using ApiGateway.Core.Filters;

namespace ApiGateway.Filters
{
    public class IPFilter : IFilter
    {
        public FilterType FilterType => FilterType.Pre;
        public int FilterOrder => 15;

        public void Execute()
        {
            string ipAddress = GetClientIPAddress();
        }

        private string GetClientIPAddress()
        {
            var request = RequestContext.Current.Request;
            return request.HttpContext.Connection.RemoteIpAddress.AddressFamily.ToString();
        }
    }
}
