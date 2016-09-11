using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

            var task = Request(uri, api.HttpMethod, context.RequestModel.Body);
            task.Wait();
            context.ResponseModel = new ApiResponseModel(200, "Success", JsonConvert.DeserializeObject(task.Result));
        }

        private async Task<string> Request(Uri uri, string httpMethod, string body)
        {
            int timeout = 5000;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var httpMethodEnum = GetHttpMethod(httpMethod);
            HttpRequestMessage message = new HttpRequestMessage(httpMethodEnum, uri);
            if (httpMethodEnum == HttpMethod.Post || httpMethodEnum == HttpMethod.Put)
            {
                byte[] requestData = Encoding.UTF8.GetBytes(body);
                message.Content = new ByteArrayContent(requestData);
            }
            // TODO: replace path params

            try
            {
                var task = client.SendAsync(message);
                if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                {
                    using (var response = task.Result)
                    {
                        response.EnsureSuccessStatusCode();

                        string text = await response.Content.ReadAsStringAsync();
                        return text;
                    }
                }
                client.CancelPendingRequests();
                throw new Exception("Timeout");
            }
            finally
            {
                client.Dispose();
            }
        }

        private HttpMethod GetHttpMethod(string httpMethod)
        {
            httpMethod = httpMethod.ToUpper();

            switch (httpMethod)
            {
                case "GET":
                    return HttpMethod.Get;
                case "POST":
                    return HttpMethod.Post;
                case "PUT":
                    return HttpMethod.Put;
                case "DELETE":
                    return HttpMethod.Delete;
                default:
                    return HttpMethod.Post;
            }
        }
    }
}
