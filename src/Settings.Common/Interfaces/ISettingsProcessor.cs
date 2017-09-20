using System.Collections.Generic;
using Settings.Common.Models;

namespace Settings.Common.Interfaces
{
    public interface ISettingsProcessor
    {
        IEnumerable<SettingReadModel> CalculateEnvironmentSettings(IEnumerable<ApplicationEnvironmentSettings> appEnvSettings,
            string applicationName, string environmentName);
    }
}
