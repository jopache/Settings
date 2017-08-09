using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Settings.Common.Domain;
using Settings.Common.Interfaces;

namespace Settings.Services
{
    public class SettingsProcessor : ISettingsProcessor
    {
        public ApplicationEnvironmentSettings CalculateEnvironmentSettings(IEnumerable<ApplicationEnvironmentSettings> appEnvSettings,
            string applicationName, string environmentName)
        {
            JObject settings = null;
            foreach (var appEnvSetting in appEnvSettings)
            {
                var configurationJson = appEnvSetting.ConfigurationJson;
                if (settings == null)
                {
                    settings = JObject.Parse(configurationJson);
                    continue;
                }

                var settingsToMergeIn = JObject.Parse(configurationJson);

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
                return null;
            }
            return new ApplicationEnvironmentSettings(settings.ToString(),
                applicationName, environmentName);
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