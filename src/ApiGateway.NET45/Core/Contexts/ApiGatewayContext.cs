using System.Collections.Concurrent;
using System.Threading;
using System.Web;
using ApiGateway.NET45.Core.Exceptions;
using ApiGateway.NET45.Core.Models;

namespace ApiGateway.NET45.Core.Contexts
{
    public class ApiGatewayContext : ConcurrentDictionary<string, object>
    {
        private static ThreadLocal<ApiGatewayContext> _instance;

        public static ApiGatewayContext Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThreadLocal<ApiGatewayContext>(() => new ApiGatewayContext());
                }
                return _instance.Value;
            }
        }

        private ApiGatewayContext()
        {
        }

        public string ApiName { get; set; }

        public string RequestBody { get; set; }

        public ApiResponseModel ResponseModel { get; set; }

        public HttpRequest Request { get; set; }

        public ApiGatewayException Exception { get; set; }

        public T GetProperty<T>(string key) where T : class
        {
            object obj;
            if (TryGetValue(key, out obj))
            {
                var value = obj as T;
                return value;
            }
            return default(T);
        }

        public void SetProperty(string key, object value)
        {
            AddOrUpdate(key, value, (s, o) => value);
        }
    }
}
