using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using Settings.Common.Interfaces;

namespace Settings.Controllers.api
{

    [Route("api/settings/")]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet("{applicationName}/{environmentName}")]
        public IActionResult GetSettingsForApplicationEnvironment(string applicationName, string environmentName)
        {
            var runningSettings = _settingsService.GetApplicationEnvironmentSettings(applicationName, environmentName);

            if (runningSettings == null)
            {
                return NotFound();
            }

            var result = runningSettings
                .ToList()
                .OrderBy(x => x.ApplicationLeftWeight)
                .ThenBy(x => x.EnvironmentLeftWeight)
                .ThenBy(x => x.Name);

            return Ok(result);
        }
    }
}
