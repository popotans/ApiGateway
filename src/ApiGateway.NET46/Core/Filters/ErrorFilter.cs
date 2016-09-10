using ApiGateway.NET46.Core.Contexts;
using ApiGateway.NET46.Core.Models;

namespace ApiGateway.NET46.Core.Filters
{
    public class ErrorFilter : IFilter
    {
        public FilterType FilterType => FilterType.Error;
        public int FilterOrder => 1;
        public void Execute()
        {
            var context = ApiGatewayContext.Current;
            var exception = context.Exception;
            
            context.ResponseModel = new ApiResponseModel(exception);
        }
    }
}
