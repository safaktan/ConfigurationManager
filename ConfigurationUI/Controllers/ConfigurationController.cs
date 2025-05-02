using ConfigurationUI.Services;
using ConfigurationUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationUI.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public async Task<IActionResult> Index(string applicationName)
        {
            if (!string.IsNullOrWhiteSpace(applicationName))
            {
                var configurationsByFilter = await _configurationService.GetConfigurationByApplicationNameAsync(applicationName);
                return View(configurationsByFilter);
            }
        
            var configurations = await _configurationService.GetAllConfigurationsAsync();
            return View(configurations);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ConfigurationViewModel configurationViewModel)
        {
            if (!ModelState.IsValid) 
                return RedirectToAction("Index");
            
             await _configurationService.SaveConfigurationAsync(configurationViewModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ConfigurationViewModel configurationViewModel)
        {
            if (!ModelState.IsValid) 
                return RedirectToAction("Index");

            await _configurationService.UpdateConfigurationAsync(configurationViewModel);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _configurationService.DeleteConfigurationAsync(id);
            return RedirectToAction("Index");
        }
    }
}