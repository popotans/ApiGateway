using ApiGateway.Core.IoC;
using ApiGateway.Core.Providers;
using ApiGateway.Domain;
using ApiGateway.Domain.Models;

namespace ApiGateway.Providers
{
    public class ApiProvider : IApiProvider
    {
        private readonly IApiRepository _repository = ObjectContainer.Resolve<IApiRepository>();

        public Api Get(string apiName)
        {
            return _repository.Get(apiName);
        }
    }
}
