using ConfigurationReader.Models;

namespace ConfigurationReader.Interfaces
{
    public interface IStorageProvider
    {
        Task<List<ConfigurationParameter>> GetConfigurationsAsync(string applicationName);
    }
}