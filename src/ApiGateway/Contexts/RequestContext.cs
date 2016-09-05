using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway.Contexts
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


    }
}
