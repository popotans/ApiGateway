using ApiGateway.NET46.Core.Contexts;
using ApiGateway.NET46.Core.Filters;

namespace ApiGateway.NET46.Sloth.Filters
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
            var request = ApiGatewayContext.Current.Request;
            string ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
