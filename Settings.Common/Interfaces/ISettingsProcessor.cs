using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Settings.Common.Domain;

namespace Settings.Common.Interfaces
{
    public interface ISettingsProcessor
    {
        ApplicationEnvironmentSettings CalculateEnvironmentSettings(IEnumerable<ApplicationEnvironmentSettings> appEnvSettings,
            string applicationName, string environmentName);
    }
}
