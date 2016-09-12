using ApiGateway.Domain.Models;

namespace ApiGateway.Domain
{
    public interface IUserRepository
    {
        User Get(string accessKey);
    }
}
