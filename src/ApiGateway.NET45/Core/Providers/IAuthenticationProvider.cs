namespace ApiGateway.NET45.Core.Providers
{
    public interface IAuthenticationProvider
    {
        bool Authenticate(string apiName, string accessKey);
    }
}
