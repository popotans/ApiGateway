using ApiGateway.NET46.Core.Contexts;
using ApiGateway.NET46.Core.Filters;

namespace ApiGateway.NET46.Sloth.Filters
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
