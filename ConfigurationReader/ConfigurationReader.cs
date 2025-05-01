using System.Reflection.Metadata;
using ConfigurationReader.Interfaces;
using ConfigurationReader.Providers;

namespace ConfigurationReader
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly IStorageProvider _storageProvider;
        private readonly string _applicationName;

        public ConfigurationReader(string applicationName, string connectionString)
        {
            _applicationName = applicationName;
            _storageProvider = StorageProviderFactory.CreateProvider(connectionString);
        }

        public T GetValue<T>(string key)
        {
            var configurations = _storageProvider.GetConfigurations(_applicationName);
            var config = configurations.FirstOrDefault(c => c.Name == key);
            if (config == null)
            {
                throw new KeyNotFoundException($"Configuration key '{key}' not found.");
            }
            return (T)Convert.ChangeType(config.Value, typeof(T));
        }

    }
}