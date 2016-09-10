using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiGateway.NET46.Core.Contexts;
using ApiGateway.NET46.Core.Filters;
using ApiGateway.NET46.Core.Models;

namespace ApiGateway.NET46.Sloth.Filters
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