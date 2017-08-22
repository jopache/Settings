using System.Collections.Generic;
using Settings.Common.Models;

namespace Settings.Common.Interfaces
{
    public interface ISettingsService
    {
        IEnumerable<SettingReadModel> GetApplicationEnvironmentSettings(string applicationName, 
            string environmentName);
    }
}