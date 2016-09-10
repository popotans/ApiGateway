using System;

namespace ApiGateway.NET46.Core.Exceptions
{
    public class ApiGatewayException : Exception
    {
        public int Code { get; }

        public ApiGatewayException(int code, string message) : base(message)
        {
            Code = code;
        }

        public ApiGatewayException(Exception e, int code, string message) : base(message, e)
        {
            Code = code;
        }
    }
}
