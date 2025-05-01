using ConfigurationReader.Models;

namespace ConfigurationReader.Interfaces
{
    public interface IStorageProvider
    {
        IEnumerable<ConfigurationParameter> GetConfigurations(string applicationName);
    }
}