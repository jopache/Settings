using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using Settings.Common.Interfaces;
using Settings.Common.Models;

namespace Settings.Controllers.api
{

    [Route("api/settings/")]
    public class SettingsController : Controller
    {
        private readonly ISettingsService _settingsService;
        private readonly IAuthorizationService _authorizationService;

        public SettingsController(ISettingsService settingsService, 
            IAuthorizationService authorizationService)
        {
            _settingsService = settingsService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{applicationName}/{environmentName}")]
        [ProducesResponseType(typeof(IEnumerable<SettingReadModel>), statusCode: 200)]
        public IActionResult GetSettingsForApplicationEnvironment(string applicationName, string environmentName)
        {
            // todo: can probably centralize this logic as it will be used in more places
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault( x => x.Type == "userId");
            var userId = userIdClaim.Value;

            var userCanReadSettings = _authorizationService
                .UserCanReadSettings(userId, applicationName, environmentName);

            // todo bleh
            if (!userCanReadSettings) {
                return Forbid("no access");
            }

            var runningSettings = _settingsService.GetApplicationEnvironmentSettings(applicationName, environmentName);

            if (runningSettings == null)
            {
                return NotFound();
            }

            var result = runningSettings
                .ToList()
                .OrderBy(x => x.ApplicationDepth)
                .ThenBy(x => x.EnvironmentDepth)
                .ThenBy(x => x.Name);

            return Ok(result);
        }

        [HttpPost("create-update/{applicationName}/{environmentName}")]
        [ProducesResponseType(typeof(Boolean), 200)]
        public IActionResult CreateOrUpdate(string applicationName, string environmentName, [FromBody] SettingsWriteModel settings)
        {
            _settingsService.CreateOrEditSettings(applicationName, environmentName, settings);
            return Ok(true);
        }
    }
}
