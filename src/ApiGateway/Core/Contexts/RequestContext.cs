using System.Collections.Concurrent;
using System.Threading;
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

        public string ApiName { get; set; }

        public string AccessKey { get; set; }

        public HttpRequest Request { get; set; }

        public T GetProperty<T>(string key) where T : class
        {
            object obj;
            if (this.TryGetValue(key, out obj))
            {
                var value = obj as T;
                return value;
            }
            return default(T);
        }

        public void SetProperty(string key, object value)
        {
            this.AddOrUpdate(key, value, (s, o) => value);
        }
    }
}
