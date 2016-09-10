using ApiGateway.NET46.Core.Providers;

namespace ApiGateway.NET46.Sloth.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public bool Authenticate(string apiName, string accessKey)
        {
            return true;
        }
    }
}
