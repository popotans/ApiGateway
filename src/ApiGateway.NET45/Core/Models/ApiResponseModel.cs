using ApiGateway.NET45.Core.Exceptions;

namespace ApiGateway.NET45.Core.Models
{
    public class ApiResponseModel
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object Data { get; set; }

        public ApiResponseModel(int statusCode, string statusMessage)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
        }

        public ApiResponseModel(ApiGatewayException exception)
        {
            StatusCode = exception.Code;
            StatusMessage = exception.Message;
        }
    }
}
