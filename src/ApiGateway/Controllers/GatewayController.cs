using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    public class GatewayController : Controller
    {
        // POST api/values
        [HttpPost]
        [Route("gateway/{Method}")]
        public void Post(ApiRequestModel model)
        {

        }
    }
}
