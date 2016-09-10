using System;
using ApiGateway.Core.Contexts;
using ApiGateway.Core.Exceptions;
using ApiGateway.Core.Filters;
using ApiGateway.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    public class GatewayController : Controller
    {
        private readonly ILogger<GatewayController> _logger;

        public GatewayController(ILogger<GatewayController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{ApiName}")]
        public ApiResponseModel Post(ApiRequestModel model)
        {
            try
            {
                try
                {
                    _logger.LogDebug("PreRoute");
                    PreRoute();
                }
                catch (ApiGatewayException e)
                {
                    Error(e);
                    PostRute();
                    return RequestContext.Current.ResponseModel;
                }

                try
                {
                    Route();
                }
                catch (ApiGatewayException e)
                {
                    Error(e);
                    PostRute();
                    return RequestContext.Current.ResponseModel;
                }

                try
                {
                    PostRute();
                }
                catch (ApiGatewayException e)
                {
                    Error(e);
                    return RequestContext.Current.ResponseModel;
                }
            }
            catch (Exception exception)
            {
                Error(new ApiGatewayException(exception, 500, "UNHANDLED_EXCEPTION_" + exception.Source));
            }

            return RequestContext.Current.ResponseModel ?? new ApiResponseModel(204, null);
        }

        private void PreRoute()
        {
            FilterRunner.PreRoute();
        }

        private void PostRute()
        {
            FilterRunner.PostRoute();
        }

        private void Route()
        {
            FilterRunner.Route();
        }

        private void Error(ApiGatewayException exception)
        {
            RequestContext.Current.Exception = exception;
            FilterRunner.Error();
        }
    }
}
