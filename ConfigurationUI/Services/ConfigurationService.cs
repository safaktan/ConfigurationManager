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
            var result =  await _configurationRepository.GetAllConfigurationsAsync();
            if (result == null)
            {
                return new List<ConfigurationViewModel>();
            }
            var configurationViewModelList = _mapper.Map<List<ConfigurationViewModel>>(result);
            return configurationViewModelList;
        }

        public async Task SaveConfigurationAsync(ConfigurationViewModel configurationViewModel)
        {
            var configurationParameter = _mapper.Map<ConfigurationParameter>(configurationViewModel);
            configurationParameter.IsActive = true;
            await _configurationRepository.SaveConfigurationAsync(configurationParameter);
        }

        public async Task<List<ConfigurationViewModel>> GetConfigurationByApplicationNameAsync(string applicationName)
        {
            var result = await _configurationRepository.GetConfigurationByApplicationNameAsync(applicationName);
            if (result == null)
            {
                return new List<ConfigurationViewModel>();
            }
            var configurationViewModelList = _mapper.Map<List<ConfigurationViewModel>>(result);
            return configurationViewModelList;
        }

        public async Task<ConfigurationViewModel> GetConfigurationByIdAsync(int id)
        {
            var result = await _configurationRepository.GetConfigurationByIdAsync(id);
            if (result == null)
            {
                return null;
            }
            var configurationViewModel = _mapper.Map<ConfigurationViewModel>(result);
            return configurationViewModel;
        }

        public async Task DeleteConfigurationAsync(int id)
        {
            await _configurationRepository.DeleteConfigurationAsync(id);
        }

        public async Task UpdateConfigurationAsync(ConfigurationViewModel configurationViewModel)
        {
            var configurationParameter = _mapper.Map<ConfigurationParameter>(configurationViewModel);
            await _configurationRepository.UpdateConfigurationAsync(configurationParameter);
        }
    }
    
}