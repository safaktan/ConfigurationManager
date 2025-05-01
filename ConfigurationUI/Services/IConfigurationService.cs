using ConfigurationUI.Models;
using ConfigurationUI.ViewModels;

namespace ConfigurationUI.Services
{
    public interface IConfigurationService
    {
        Task<List<ConfigurationViewModel>> GetAllConfigurationsAsync();
        Task SaveConfigurationAsync(ConfigurationViewModel configurationViewModel);
        Task<List<ConfigurationViewModel>> GetConfigurationByApplicationNameAsync(string applicationName);
        Task<ConfigurationViewModel> GetConfigurationByIdAsync(int id);
        Task DeleteConfigurationAsync(int id);
        Task UpdateConfigurationAsync(ConfigurationViewModel configurationViewModel);
    }
    
}