namespace ApiGateway.Core.Providers
{
    public interface IAuthenticationProvider
    {
        bool Authenticate(string apiName, string accessKey);
    }
}
