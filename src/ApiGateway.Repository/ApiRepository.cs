using System;
using ApiGateway.Domain;
using ApiGateway.Domain.Models;

namespace ApiGateway.Repository
{
    public class ApiRepository : IApiRepository
    {
        public Api Get(string apiName)
        {
            string key = CacheHelper.MakeCacheKey(CacheKeys.Api, apiName);
            var entries = Redis.Db.HashGetAll(key);
            if (entries == null || entries.Length == 0)
                throw new Exception($"Cannot Find Api {apiName}");

            var api = entries.ToObject<Api>();
            return api;
        }
    }
}
