namespace ApiGateway.Core.Models
{
    public class ApiResponseModel
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public ApiResponseModel(int statusCode, string statusMessage)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage;
        }
    }
}
