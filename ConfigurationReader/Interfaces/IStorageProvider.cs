using ConfigurationReaderLibrary.Models;

namespace ConfigurationReaderLibrary.Interfaces
{
    public interface IStorageProvider
    {
        Task<List<ConfigurationParameter>> GetConfigurationsAsync(string applicationName);
    }
}