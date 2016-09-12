using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ApiGateway.Core.IoC;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ApiGateway.Repository
{
    public static class Redis
    {
        private static readonly ConnectionMultiplexer Connection;

        static Redis()
        {
            var configuration = ObjectContainer.Resolve<IConfiguration>();
            string redisConnectionString = configuration.GetConnectionString("RedisConnection");
            Connection = ConnectionMultiplexer.Connect(redisConnectionString);
        }

        public static IDatabase Db => Connection.GetDatabase(0);

        public static IDatabase GetDatabase(int index)
        {
            return Connection.GetDatabase(0);
        }

        public static HashEntry[] ToHashEntries<T>(this T t)
        {
            var properties = typeof(T).GetProperties();
            List<HashEntry> entries = new List<HashEntry>();
            foreach (var property in properties)
            {
                var value = property.GetValue(t);
                entries.Add(new HashEntry(property.Name, JsonConvert.SerializeObject(value)));
            }
            return entries.ToArray();
        }

        public static T ToObject<T>(this HashEntry[] entries)
        {
            var type = typeof(T);
            var instance = (T) Activator.CreateInstance(type);
            var properties = typeof(T).GetProperties();
            foreach (var entry in entries)
            {
                var property = properties.SingleOrDefault(i => i.Name == entry.Name);
                property?.SetValue(instance, JsonConvert.DeserializeObject(entry.Value.ToString(), property.PropertyType));
            }

            return instance;
        }
    }

    public static class CacheKeys
    {
        public const string Api = "api:{0}";
    }

    public static class CacheHelper
    {
        private const string KeyPrefix = "apigateway:";

        public static string MakeCacheKey(string key)
        {
            return string.Concat(KeyPrefix, key);
        }

        public static string MakeCacheKey(string format, params object[] args)
        {
            return string.Concat(KeyPrefix, string.Format(format, args));
        }
    }
}
