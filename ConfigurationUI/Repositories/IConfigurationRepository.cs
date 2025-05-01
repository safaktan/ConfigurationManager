using ConfigurationUI.Models;

namespace ConfigurationUI.Repositories
{
    public interface IConfigurationRepository
    {
        Task<List<ConfigurationParameter>> GetAllConfigurationsAsync();
        Task SaveConfigurationAsync(ConfigurationParameter configurationParameter);
        Task<List<ConfigurationParameter>> GetConfigurationByApplicationNameAsync(string applicationName);
        Task<ConfigurationParameter> GetConfigurationByIdAsync(int id);
        Task DeleteConfigurationAsync(int id);
        Task UpdateConfigurationAsync(ConfigurationParameter configurationParameter);
    }
    
}