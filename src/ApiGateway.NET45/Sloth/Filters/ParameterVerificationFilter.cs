using ApiGateway.NET45.Core.Contexts;
using ApiGateway.NET45.Core.Filters;

namespace ApiGateway.NET45.Sloth.Filters
{
    public class ParameterVerificationFilter : IFilter
    {
        public FilterType FilterType => FilterType.Pre;
        public int FilterOrder => 20;

        public void Execute()
        {
            var request = ApiGatewayContext.Current;

        }
    }
}
