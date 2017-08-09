using Newtonsoft.Json.Linq;
using Settings.Common.Domain;

namespace Settings.Common.Interfaces
{
    public interface ISettingsService
    {
        ApplicationEnvironmentSettings GetApplicationEnvironmentSettings(string applicationName, 
            string environmentName);
    }
}