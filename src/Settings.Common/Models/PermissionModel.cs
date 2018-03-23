using System.Linq;

namespace Settings.Common.Models
{
    public class PermissionModel {
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public bool CanDecrypt { get; set; }
        public bool CanAddChildren { get; set; }
        public int ApplicationId { get; set; }
        public int EnvironmentId { get; set; }
    }
}