using System;
using ApiGateway.Core.Contexts;
using ApiGateway.Core.Exceptions;
using ApiGateway.Core.Filters;
using ApiGateway.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/[controller]")]
    public class GatewayController : Controller
    {
        [HttpPost("{ApiName}")]
        public ApiResponseModel Post(ApiRequestModel model)
        {
            try
            {
                try
                {
                    PreRoute();
                }
                catch (ApiGatewayException e)
                {
                    Error(e);
                    PostRute();
                }

                try
                {
                    Route();
                }
                catch (ApiGatewayException e)
                {
                    Error(e);
                    PostRute();
                }

                try
                {
                    PostRute();
                }
                catch (ApiGatewayException e)
                {
                    Error(e);
                }
            }
            catch (Exception exception)
            {
                Error(new ApiGatewayException(exception, 500, "UNHANDLED_EXCEPTION_" + exception.Source));
            }

            return RequestContext.Current.ResponseModel ?? new ApiResponseModel(500, model.ApiName);
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
