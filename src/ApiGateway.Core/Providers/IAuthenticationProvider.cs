using System;

namespace ApiGateway.Core.Providers
{
    public interface IAuthenticationProvider
    {
        bool Authenticate(string apiName, string accessKey);

        bool CheckSign(string accessKey, DateTime timestamp, string body, string sign);
    }
}
