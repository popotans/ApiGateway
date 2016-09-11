using ApiGateway.Core.Providers;

namespace ApiGateway.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public bool Authenticate(string apiName, string accessKey)
        {
            return true;
        }
    }
}
