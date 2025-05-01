using ConfigurationReader.Interfaces;
using ConfigurationReader.Models;
using ConfigurationReader.Providers;

namespace ConfigurationReader
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly IStorageProvider _storageProvider;
        private readonly string _applicationName;
        private readonly int _refreshIntervalInMs;
        private readonly Timer _refreshTimer;
        private static Dictionary<string, ConfigurationParameter> _cache = new Dictionary<string, ConfigurationParameter>();
        private static readonly object _cacheLock = new object();

        public ConfigurationReader(string applicationName, string connectionString, int refreshIntervalInMs)
        {
            _refreshIntervalInMs = refreshIntervalInMs;
            _applicationName = applicationName;
            _storageProvider = StorageProviderFactory.CreateProvider(connectionString);

            _refreshTimer = new Timer(async _ => await RefreshConfigurations(), null, 0, _refreshIntervalInMs);

        }

        public T GetValue<T>(string key)
        {
             if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            try
            {
                if (_cache.TryGetValue(key, out var configItem))
                {
                    return (T)Convert.ChangeType(configItem.Value, typeof(T));
                }
                Console.WriteLine($"Configuration key '{key}' not found in cache. Attempting to refresh configurations.");
                throw new KeyNotFoundException($"Configuration key '{key}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting configuration value for key '{key}': {ex.Message}");
                throw new InvalidOperationException($"Error converting configuration value for key '{key}' to type '{typeof(T)}'.", ex);
            }
        }

        private async Task RefreshConfigurations()
        {
            try
            {
                var configurations = await _storageProvider.GetConfigurationsAsync(_applicationName);
                lock (_cacheLock)
                {
                    _cache.Clear();
                    foreach (var config in configurations)
                    {
                        _cache[config.Name] = config;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error refreshing configurations: {ex.Message}");
            }
        }

    }
}