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

        public IEnumerable<ConfigurationParameter> GetConfigurations(string applicationName)
        {
            var db = _redis.GetDatabase();
            var hashEntries = db.HashGetAll(applicationName);

            return hashEntries.Select(entry => new ConfigurationParameter
            {
                Name = entry.Name,
                Value = entry.Value,
                IsActive = true,
                ApplicationName = applicationName
            });
        }
    }


}