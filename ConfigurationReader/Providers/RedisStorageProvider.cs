using ConfigurationReader.Interfaces;
using ConfigurationReader.Models;
using StackExchange.Redis;

namespace ConfigurationReader.Providers
{
    public class RedisStorageProvider : IStorageProvider
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisStorageProvider(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
        }

        public Task<List<ConfigurationParameter>> GetConfigurationsAsync(string applicationName)
        {
            var db = _redis.GetDatabase();
            var hashEntries = db.HashGetAll(applicationName);

            return Task.FromResult(hashEntries.Select(entry => new ConfigurationParameter
            {
                Name = entry.Name,
                Value = entry.Value,
                IsActive = true,
                ApplicationName = applicationName
            }).ToList());
        }
    }


}