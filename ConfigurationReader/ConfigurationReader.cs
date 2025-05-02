using System.Globalization;
using ConfigurationReaderLibrary.Interfaces;
using ConfigurationReaderLibrary.Models;
using ConfigurationReaderLibrary.Providers;

namespace ConfigurationReaderLibrary
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly IStorageProvider _storageProvider;
        private readonly string _applicationName;
        private readonly int _refreshIntervalInMs;
        private static Dictionary<string, ConfigurationParameter> _cache = new Dictionary<string, ConfigurationParameter>();
        private static readonly object _cacheLock = new object();

        public ConfigurationReader(string applicationName, string connectionString, int refreshIntervalInMs)
        {
            _refreshIntervalInMs = refreshIntervalInMs;
            _applicationName = applicationName;
            _storageProvider = StorageProviderFactory.CreateProvider(connectionString);

            _ = StartBackgroundRefresh();

        }

        public T GetValue<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                Console.WriteLine("Configuration key cannot be null or empty.");

            if (_cache.TryGetValue(key, out var configItem))
            {
                try
                {
                    object rawValue = configItem.Value;

                    if (typeof(T) == typeof(bool))
                    {
                        if (rawValue.ToString() == "1") return (T)(object)true;
                        if (rawValue.ToString() == "0") return (T)(object)false;
                        return (T)(object)bool.Parse(rawValue.ToString());
                    }
                    else if (typeof(T) == typeof(double))
                    {
                        var stringValue = rawValue.ToString();
                        if (stringValue.Contains(","))
                        {
                            stringValue = stringValue.Replace(",", ".");
                        }
                        return (T)(object)double.Parse(stringValue, CultureInfo.InvariantCulture);
                    }

                    return (T)Convert.ChangeType(rawValue, typeof(T));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting configuration value for key '{key}': {ex.Message}");
                   return  default;
                }
            }
            return default;
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

        private async Task StartBackgroundRefresh()
        {
            var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(_refreshIntervalInMs));

            while (await timer.WaitForNextTickAsync())
            {
                try
                {
                    await RefreshConfigurations();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[PeriodicTimer ERROR] {ex.Message}");
                }
            }
        }
    }
}