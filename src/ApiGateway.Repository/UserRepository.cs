using System;
using ApiGateway.Domain;
using ApiGateway.Domain.Models;

namespace ApiGateway.Repository
{
    public class UserRepository : IUserRepository
    {
        public User Get(string accessKey)
        {
            string key = CacheHelper.MakeCacheKey(CacheKeys.User, accessKey);
            var entries = Redis.Db.HashGetAll(key);

            if (entries == null || entries.Length == 0)
                throw new Exception($"Cannot find user {accessKey}");

            var user = entries.ToObject<User>();
            if (user == null)
                throw new Exception($"Cannot find user {accessKey}");

            return user;
        }
    }
}
