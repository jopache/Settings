namespace Settings.Common.Models
{
    //TODO: Figure out class namings based upon what your classes are actually doing
    public class ApplicationEnvironmentSettings
    {
        public string ConfigurationJson { get; set; }
        public string ApplicationName { get; set; }
        public string EnvironmentName { get; set; }
        public int ApplicationId { get; set; }
        public int EnvironmentId { get; set; }

        public int EnvironmentDepth { get; set; }
        public int ApplicationDepth { get; set; }
    }

}
