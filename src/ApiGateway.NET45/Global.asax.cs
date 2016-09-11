using System.Web;
using System.Web.Http;
using ApiGateway.NET45.Core.Filters;
using Newtonsoft.Json.Serialization;

namespace ApiGateway.NET45
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
              new CamelCasePropertyNamesContractResolver();

            FilterLoader.Load();
        }
    }
}
