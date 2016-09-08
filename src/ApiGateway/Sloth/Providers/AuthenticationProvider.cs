using ApiGateway.Core.Providers;

namespace ApiGateway.Sloth.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        public bool Authenticate(string apiName, string accessKey)
        {
            return true;
        }
    }
}
