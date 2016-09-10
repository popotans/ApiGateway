using System;

namespace ApiGateway.Core.Exceptions
{
    public class ApiGatewayException : Exception
    {
        private int _code;

        public ApiGatewayException(int code, string message) : base(message)
        {
            _code = code;
        }

        public ApiGatewayException(Exception e, int code, string message) : base(message, e)
        {
            _code = code;
        }
    }
}
