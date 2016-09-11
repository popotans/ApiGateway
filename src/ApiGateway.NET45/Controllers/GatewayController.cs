using System;
using System.Web;
using System.Web.Http;
using ApiGateway.NET45.Core.Contexts;
using ApiGateway.NET45.Core.Exceptions;
using ApiGateway.NET45.Core.Filters;
using ApiGateway.NET45.Core.Models;

namespace ApiGateway.NET45.Controllers
{
    public class GatewayController : ApiController
    {
        [HttpPost]
        [Route("gateway/{apiName}")]
        public ApiResponseModel Post(string apiName, [FromBody]string body)
        {
            Init(apiName, body);
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
                    return ApiGatewayContext.Current.ResponseModel;
                }

                try
                {
                    Route();
                }
                catch (ApiGatewayException e)
                {
                    Error(e);
                    PostRute();
                    return ApiGatewayContext.Current.ResponseModel;
                }

                try
                {
                    PostRute();
                }
                catch (ApiGatewayException e)
                {
                    Error(e);
                    return ApiGatewayContext.Current.ResponseModel;
                }
            }
            catch (Exception exception)
            {
                Error(new ApiGatewayException(exception, 500, "UNHANDLED_EXCEPTION_" + exception.Source));
            }

            return ApiGatewayContext.Current.ResponseModel ?? new ApiResponseModel(204, null);
        }

        private void Init(string apiName, string body)
        {
            var context = ApiGatewayContext.Current;
            context.Request = HttpContext.Current.Request;
            context.ApiName = apiName;
            context.RequestBody = body;
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
            ApiGatewayContext.Current.Exception = exception;
            FilterRunner.Error();
        }
    }
}
