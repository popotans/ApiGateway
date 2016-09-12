using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Filters
{
    public class HttpClientHelper
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="body"></param>
        /// <param name="timeout">ms</param>
        /// <returns></returns>
        public static async Task<string> Request(Uri uri, string httpMethod, string body, int timeout)
        {
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var httpMethodEnum = GetHttpMethod(httpMethod);
            HttpRequestMessage message = new HttpRequestMessage(httpMethodEnum, uri);
            if (httpMethodEnum == HttpMethod.Post || httpMethodEnum == HttpMethod.Put)
            {
                byte[] requestData = Encoding.UTF8.GetBytes(body);
                message.Content = new ByteArrayContent(requestData);
            }
            // TODO: replace path params

            var task = HttpClient.SendAsync(message);
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                using (var response = task.Result)
                {
                    response.EnsureSuccessStatusCode();

                    string text = await response.Content.ReadAsStringAsync();
                    return text;
                }
            }
            HttpClient.CancelPendingRequests();
            throw new Exception("Timeout");
        }

        private static HttpMethod GetHttpMethod(string httpMethod)
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
