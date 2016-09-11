using ApiGateway.Core.Contexts;
using ApiGateway.Core.Filters;

namespace ApiGateway.Filters
{
    public class ParameterVerificationFilter : IFilter
    {
        public FilterType FilterType => FilterType.Pre;
        public int FilterOrder => 20;

        public void Execute()
        {
            var request = RequestContext.Current;

        }
    }
}
