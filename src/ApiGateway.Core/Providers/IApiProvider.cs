using ApiGateway.Domain.Models;

namespace ApiGateway.Core.Providers
{
    public interface IApiProvider
    {
        Api Get(string apiName);
    }
}
