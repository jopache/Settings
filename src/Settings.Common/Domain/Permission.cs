namespace Settings.Common.Domain {
    public class Permission {
        public int Id { get; set; }
        //todo: turn this in to an int?  all the setup in startup.cs was giving me errors when attemping to do so. 
        public string UserId { get; set; }
        public int ApplicationId { get; set; }
        public int EnvironmentId { get; set; }
        public virtual Application Application { get; set; }
        public virtual Environment Environment { get; set; }
        public bool CanReadSettings { get; set; }
        public bool CanWriteSettings { get; set; }
        public bool CanCreateChildEnvironments { get; set; }
        public bool CanCreateChildApplications { get; set; }
        public bool CanDecryptSetting { get; set; }
    }
}