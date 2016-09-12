using System;
using ApiGateway.Core.IoC;
using ApiGateway.Core.Providers;
using ApiGateway.Domain;

namespace ApiGateway.Providers
{
    public class AuthenticationProvider : IAuthenticationProvider
    {
        private readonly IUserRepository _userRepository = ObjectContainer.Resolve<IUserRepository>();

        public bool Authenticate(string apiName, string accessKey)
        {
            return true;
        }

        public bool CheckSign(string accessKey, DateTime timestamp, string body, string sign)
        {
            var user = _userRepository.Get(accessKey);
            var hash =
                EncryptHelper.SHA1Encrypt(string.Concat(accessKey, timestamp.ToString("yyyy-MM-dd HH:mm:ss"), body,
                    user.AccessSecret));
            return hash == sign;
        }
    }
}
