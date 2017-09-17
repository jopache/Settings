namespace Settings.Common.Domain
{
    public class Setting
    {
        public int Id { get; set; }

        public string Contents { get; set; }

        public virtual Application Application { get; set; }

        public virtual Environment Environment { get; set; }

        public int ApplicationId { get; set; }

        public int EnvironmentId { get; set; }
    }
}
