using ApiGateway.Core.Filters;
using ApiGateway.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    public class GatewayController : Controller
    {
        // POST api/values
        [HttpPost]
        [Route("gateway/{Method}")]
        public ApiResponseModel Post(ApiRequestModel model)
        {
            return null;
        }

        private void Init()
        {
            FilterLoader.Instance.Load();
        }
    }
}
