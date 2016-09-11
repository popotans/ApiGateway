using ApiGateway.Core.Contexts;
using ApiGateway.Core.Filters;
using ApiGateway.Core.Models;

namespace ApiGateway.Filters
{
    public class ErrorFilter : IFilter
    {
        public FilterType FilterType => FilterType.Error;
        public int FilterOrder => 1;
        public void Execute()
        {
            var context = RequestContext.Current;
            var exception = context.Exception;
            
            context.ResponseModel = new ApiResponseModel(exception);
        }
    }
}
