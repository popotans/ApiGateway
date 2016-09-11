using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Core.Contexts;
using ApiGateway.Core.Filters;
using ApiGateway.Core.Models;

namespace ApiGateway.Sloth.Filters
{
    public class RouteFilter : IFilter
    {
        public FilterType FilterType => FilterType.Route;
        public int FilterOrder => 1;
        public void Execute()
        {
            var context = RequestContext.Current;
            context.ResponseModel = new ApiResponseModel(200, null);
        }
    }
}
