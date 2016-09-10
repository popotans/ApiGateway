﻿using ApiGateway.Core.Exceptions;

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

        public ApiResponseModel(ApiGatewayException exception)
        {
            StatusCode = exception.Code;
            StatusMessage = exception.Message;
        }
    }
}
