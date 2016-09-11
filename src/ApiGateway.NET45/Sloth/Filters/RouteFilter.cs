using ApiGateway.NET45.Core.Contexts;
using ApiGateway.NET45.Core.Filters;
using ApiGateway.NET45.Core.Models;

namespace ApiGateway.NET45.Sloth.Filters
{
    public class RouteFilter : IFilter
    {
        public FilterType FilterType => FilterType.Route;
        public int FilterOrder => 1;
        public void Execute()
        {
            var context = ApiGatewayContext.Current;
            context.ResponseModel = new ApiResponseModel(200, "成功");
        }
    }
}