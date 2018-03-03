using System;
using System.Collections.Generic;
using System.Text;

namespace Settings.Common.Models
{
    //TODO: This is also a good candidate for a rename
    public class SettingReadModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int ApplicationId { get; set; }
        public int EnvironmentId { get; set; }
        public string ApplicationName { get; set; }
        public string EnvironmentName { get; set; }
        public int ApplicationDepth { get; set; }
        public int EnvironmentDepth { get; set; }
    }
}
