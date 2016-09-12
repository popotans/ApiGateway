using System;
using ApiGateway.Core.Contexts;
using ApiGateway.Core.Filters;
using ApiGateway.Core.IoC;
using ApiGateway.Core.Models;
using ApiGateway.Core.Providers;
using Newtonsoft.Json;

namespace ApiGateway.Filters
{
    public class RouteFilter : IFilter
    {
        private readonly IApiProvider _provider = ObjectContainer.Resolve<IApiProvider>();

        public FilterType FilterType => FilterType.Route;
        public int FilterOrder => 1;

        public void Execute()
        {
            var context = RequestContext.Current;
            var api = _provider.Get(context.RequestModel.ApiName);

            Uri uri;
            try
            {
                uri = new Uri(api.ServiceAddress, UriKind.Absolute);
            }
            catch (UriFormatException)
            {
                throw new Exception($"Cannot Find Api {context.RequestModel.ApiName}");
            }

            // TODO: Timeout need to be allowed to set manually
            int timeout = 5000;

            var task = HttpClientHelper.Request(uri, api.HttpMethod, context.RequestModel.Body, timeout);
            task.Wait();

            context.ResponseModel = new ApiResponseModel(200, "Success", JsonConvert.DeserializeObject(task.Result));
        }
    }
}
