using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiGateway.Core.Models;
using ApiGateway.Core.Contexts;
using ApiGateway.Core.Exceptions;
using ApiGateway.Core;

namespace ApiGateway.Controllers
{
    [Route("[controller]")]
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
            var context = RequestContext.Current;
            context.RequestModel = model;
            context.Request = Request;

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
                var e = exception.InnerException ?? exception;
                Error(new ApiGatewayException(e, 500, e.Message));
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
