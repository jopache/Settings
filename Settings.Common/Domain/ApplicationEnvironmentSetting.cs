namespace Settings.Common.Domain
{
    public class ApplicationEnvironmentSettings
    {
        public string ConfigurationJson { get; set; }
        public string ApplicationName { get; set; }
        public string EnvironmentName { get; set; }

        public ApplicationEnvironmentSettings(string configJson, string appName, string envName)
        {
            ConfigurationJson = configJson;
            ApplicationName = appName;
            EnvironmentName = envName;
        }
    }

}
