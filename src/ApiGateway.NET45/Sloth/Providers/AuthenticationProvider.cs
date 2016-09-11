using ApiGateway.NET45.Core.Providers;

namespace ApiGateway.NET45.Sloth.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public bool Authenticate(string apiName, string accessKey)
        {
            return true;
        }
    }
}
