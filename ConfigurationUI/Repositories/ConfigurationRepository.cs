using ConfigurationUI.Data;
using ConfigurationUI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationUI.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly ConfigDbContext _context;

        public ConfigurationRepository(ConfigDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConfigurationParameter>> GetAllConfigurationsAsync()
        {
            return await _context.ConfigurationParameters.ToListAsync();
        }

        public async Task SaveConfigurationAsync(ConfigurationParameter configurationParameter)
        {
            await _context.ConfigurationParameters.AddAsync(configurationParameter);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ConfigurationParameter>> GetConfigurationByApplicationNameAsync(string applicationName)
        {
            return await _context.ConfigurationParameters
                .Where(c => c.ApplicationName == applicationName).ToListAsync();
        }

        public async Task<ConfigurationParameter> GetConfigurationByIdAsync(int id)
        {
            return await _context.ConfigurationParameters.FindAsync(id);
        }

        public async Task DeleteConfigurationAsync(int id)
        {
            var configuration = await GetConfigurationByIdAsync(id);
            if (configuration != null)
            {
                _context.ConfigurationParameters.Remove(configuration);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateConfigurationAsync(ConfigurationParameter configurationParameter)
        {
            _context.ConfigurationParameters.Update(configurationParameter);
            await _context.SaveChangesAsync();
        }
    }
    
}