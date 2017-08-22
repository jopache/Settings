﻿using System.Collections.Generic;
using System.Linq;
using Settings.Common.Interfaces;
using Settings.Common.Models;

namespace Settings.Services
{
    public class SettingsService : ISettingsService
    {
        protected readonly ISettingsDbContext _context;
        protected readonly ISettingsProcessor _settingsProcessor;
        public SettingsService(ISettingsDbContext context, ISettingsProcessor settingsProcessor)
        {
            _context = context;
            _settingsProcessor = settingsProcessor;
        }
        public IEnumerable<SettingReadModel> GetApplicationEnvironmentSettings(string applicationName, 
            string environmentName)
        {
            var requestedApplication = _context.Applications
               .Select(x => new { x.Id, x.Name, x.LeftWeight, x.RightWeight })
               .FirstOrDefault(x => x.Name == applicationName);

            var requestedEnvironment = _context.Environments
                .Select(x => new { x.Id, x.Name, x.LeftWeight, x.RightWeight })
                .FirstOrDefault(x => x.Name == environmentName);

            if (requestedApplication == null || requestedEnvironment == null)
            {
                return null;
            }

            var appEnvSettingsList = (from app in _context.Applications
                                      join setting in _context.Settings on app.Id equals setting.ApplicationId
                                      join env in _context.Environments on setting.EnvironmentId equals env.Id
                                      where app.LeftWeight <= requestedApplication.LeftWeight && app.RightWeight >= requestedApplication.RightWeight
                                      && env.LeftWeight <= requestedEnvironment.LeftWeight && env.RightWeight >= requestedEnvironment.RightWeight
                                      orderby app.LeftWeight, env.LeftWeight
                                      select new ApplicationEnvironmentSettings
                                      {
                                          ApplicationId = app.Id,
                                          ApplicationLeftWeight = app.LeftWeight,
                                          ApplicationName = app.Name,
                                          EnvironmentId = env.Id,
                                          EnvironmentLeftWeight = env.LeftWeight,
                                          EnvironmentName = env.Name,
                                          ConfigurationJson = setting.Contents
                                      }).ToList();

            if (!appEnvSettingsList.Any())
            {
                return Enumerable.Empty<SettingReadModel>();
            }

            return _settingsProcessor.CalculateEnvironmentSettings(appEnvSettingsList, applicationName, environmentName);
        }
    }
}
