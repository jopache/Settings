﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Settings.Common.Domain;
using Settings.Common.Interfaces;
using Settings.Common.Models;
using Settings.DataAccess;

namespace Settings.Services
{
    public class SettingsService : ISettingsService
    {
        protected readonly ISettingsDbContext _context;
        protected readonly ISettingsProcessor _settingsProcessor;

        protected Queries _queries { get; }

        public SettingsService(ISettingsDbContext context, ISettingsProcessor settingsProcessor, Queries queries)
        {
            _context = context;
            _settingsProcessor = settingsProcessor;
            _queries = queries;
        }
        //TODO: Look at queries generated by this statment.  Something seems off about them
        //but it could just be that its not spitting out the whole thing to the console.
        public IEnumerable<SettingReadModel> GetApplicationEnvironmentSettings(string applicationName, 
            string environmentName)
        {
            var requestedApplication = _context.Applications
               .Select(x => new { x.Id, x.Name, x.ParentId})
               .FirstOrDefault(x => x.Name == applicationName);

            var requestedEnvironment = _context.Environments
                .Select(x => new { x.Id, x.Name, x.ParentId})
                .FirstOrDefault(x => x.Name == environmentName);

            if (requestedApplication == null || requestedEnvironment == null)
            {
                // todo: better way to handle? 
                return null;
            }

            var appHierarchyModel = _queries.LoadApplicationAndChildren()

            

            throw new NotImplementedException();
/* 
            var appEnvSettingsList = (from app in _context.Applications
                                      join setting in _context.Settings on app.Id equals setting.ApplicationId
                                      join env in _context.Environments on setting.EnvironmentId equals env.Id
                                      where app.LeftWeight <= requestedApplication.LeftWeight 
                                        && app.RightWeight >= requestedApplication.RightWeight
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
        */
        
        }

        public void CreateOrEditSettings(string applicationName, string environmentName, SettingsWriteModel writeModel)
        {
            var app = _context.Applications.FirstOrDefault(x => x.Name == applicationName);
            var env = _context.Environments.FirstOrDefault(x => x.Name == environmentName);

            var settingsEntry = _context.Settings.FirstOrDefault(x => x.EnvironmentId == env.Id
                                                                 && x.ApplicationId == app.Id);

            if (settingsEntry == null)
            {
                settingsEntry = new Setting
                {
                    EnvironmentId = env.Id,
                    ApplicationId = app.Id,
                    Contents = "{}"
                };
            }

            var settingsObj = JObject.Parse(settingsEntry.Contents);

            foreach (var settingEntry in writeModel.SettingsToUpdate)
            {
                settingsObj[settingEntry.Name] = settingEntry.Value;
            }
            settingsEntry.Contents = settingsObj.ToString();
            //todo: This is ugly, revise
            if (settingsEntry.Id == 0)
            {
                _context.AddEntity(settingsEntry);
            }
            _context.SaveChanges();

        }
    }
}
