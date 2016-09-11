using ApiGateway.Domain.Models;

namespace ApiGateway.Domain
{
    public interface IApiRepository
    {
        Api Get(string apiName);
    }
}
