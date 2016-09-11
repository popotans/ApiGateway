using System.Collections.Concurrent;
using System.Threading;
using ApiGateway.Core.Exceptions;
using ApiGateway.Core.Models;
using Microsoft.AspNetCore.Http;

namespace ApiGateway.Core.Contexts
{
    public class RequestContext : ConcurrentDictionary<string, object>
    {
        private static ThreadLocal<RequestContext> _instance;

        public static RequestContext Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThreadLocal<RequestContext>(() => new RequestContext());
                }
                return _instance.Value;
            }
        }

        private RequestContext()
        {
        }

        public ApiRequestModel RequestModel { get; set; }

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

        public void Cleanup()
        {
            _instance.Dispose();
        }
    }
}
