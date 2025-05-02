using AutoMapper;
using ConfigurationUI.Models;
using ConfigurationUI.Repositories;
using ConfigurationUI.Services;
using ConfigurationUI.ViewModels;

namespace ConfigurationUI.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IMapper _mapper;

        public ConfigurationService(IConfigurationRepository configurationRepository, IMapper mapper)
        {
            _configurationRepository = configurationRepository;
            _mapper = mapper;
        }

        public async Task<List<ConfigurationViewModel>> GetAllConfigurationsAsync()
        {
            try
            {
                var result = await _configurationRepository.GetAllConfigurationsAsync();
                if (result == null)
                {
                    return new List<ConfigurationViewModel>();
                }
                var configurationViewModelList = _mapper.Map<List<ConfigurationViewModel>>(result);
                return configurationViewModelList;
            }
            catch (System.Exception ex)
            {
                throw new Exception("An error occurred while retrieving configurations.", ex);
            }
        }

        public async Task SaveConfigurationAsync(ConfigurationViewModel configurationViewModel)
        {
            try
            {
                var configurationParameter = _mapper.Map<ConfigurationParameter>(configurationViewModel);
                configurationParameter.IsActive = true;
                await _configurationRepository.SaveConfigurationAsync(configurationParameter);
            }
            catch (System.Exception ex)
            {
                throw new Exception("An error occurred while saving the configuration.", ex);
            }
        }

        public async Task<List<ConfigurationViewModel>> GetConfigurationByApplicationNameAsync(string applicationName)
        {
            try
            {
                var result = await _configurationRepository.GetConfigurationByApplicationNameAsync(applicationName);
                if (result == null)
                {
                    return new List<ConfigurationViewModel>();
                }
                var configurationViewModelList = _mapper.Map<List<ConfigurationViewModel>>(result);
                return configurationViewModelList;
            }
            catch (System.Exception ex)
            {
                throw new Exception("An error occurred while retrieving configurations by application name.", ex);
            }
        }

        public async Task<ConfigurationViewModel> GetConfigurationByIdAsync(int id)
        {
            try
            {
                var result = await _configurationRepository.GetConfigurationByIdAsync(id);
                if (result == null)
                {
                    return null;
                }
                var configurationViewModel = _mapper.Map<ConfigurationViewModel>(result);
                return configurationViewModel;
            }
            catch (System.Exception ex)
            {
                throw new Exception("An error occurred while retrieving the configuration by ID.", ex);
            }
        }

        public async Task DeleteConfigurationAsync(int id)
        {
            try
            {
                await _configurationRepository.DeleteConfigurationAsync(id);
            }
            catch (System.Exception ex)
            {
                throw new Exception("An error occurred while deleting the configuration.", ex);
            }
        }

        public async Task UpdateConfigurationAsync(ConfigurationViewModel configurationViewModel)
        {
            try
            {
                var configurationParameter = _mapper.Map<ConfigurationParameter>(configurationViewModel);
                await _configurationRepository.UpdateConfigurationAsync(configurationParameter);
            }
            catch (System.Exception ex)
            {
                throw new Exception("An error occurred while updating the configuration.", ex);
            }
        }
    }
}