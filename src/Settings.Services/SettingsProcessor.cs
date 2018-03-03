using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Settings.Common.Interfaces;
using Settings.Common.Models;

namespace Settings.Services
{
    public class SettingsProcessor : ISettingsProcessor
    {
        public IEnumerable<SettingReadModel> CalculateEnvironmentSettings(IEnumerable<ApplicationEnvironmentSettings> appEnvSettings,
            string applicationName, string environmentName)
        {
            JObject settings = null;
            //TODO: At this point we could just deal with Dictionary objects rather than parsing
            //the jobject to strongly typed and back, but this is simpler and already done for now
            foreach (var appEnvSetting in appEnvSettings)
            {
                if (settings == null)
                {
                    settings = ProjectConfigurationIntoSettingReadModel(appEnvSetting);
                    continue;
                }

                var settingsToMergeIn = ProjectConfigurationIntoSettingReadModel(appEnvSetting);

                if (settingsToMergeIn != null)
                {
                    settings.Merge(settingsToMergeIn, new JsonMergeSettings
                    {
                        MergeArrayHandling = MergeArrayHandling.Replace
                    });
                }
            }
            if (settings == null)
            {
                return Enumerable.Empty<SettingReadModel>();
            }

            var settingsList = new List<SettingReadModel>();
            foreach (var token in settings)
            {
                settingsList.Add(token.Value.ToObject<SettingReadModel>());
            }
            return settingsList;
        }

        private JObject ProjectConfigurationIntoSettingReadModel(ApplicationEnvironmentSettings settings)
        {
            var source = JObject.Parse(settings.ConfigurationJson);
            var projected = new JObject();

            foreach (var x in source)
            {
                //TODO: Can centralize this logic using automapper
                var settingReadModel = new SettingReadModel
                {
                    ApplicationId = settings.ApplicationId,
                    ApplicationName = settings.ApplicationName,
                    ApplicationDepth = settings.ApplicationDepth,
                    EnvironmentId = settings.EnvironmentId,
                    EnvironmentName = settings.EnvironmentName,
                    EnvironmentDepth = settings.EnvironmentDepth,
                    Name = x.Key,
                    Value = x.Value.ToString()
                };
                projected.Add(x.Key, JObject.FromObject(settingReadModel));
            }
            return projected;
        }

        //TODO: Confusion with generics here. need to implement on add
        //public void CalculateWeights<T>(ref int current, IHierarchyItem<T> item)
        //{
        //    item.LeftWeight = current;

        //    foreach (var child in item.Children)
        //    {
        //        current++;
        //        CalculateWeights<T>(ref current, child);
        //    }
        //    current++;
        //}
    }
}