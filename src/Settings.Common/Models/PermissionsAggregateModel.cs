using System.Collections.Generic;
using System.Linq;

namespace Settings.Common.Models
{
    public class PermissionsAggregateModel {
        public bool CanReadAtAnyRelatedNode { 
            get {
                return Permissions.Any(x => x.CanRead == true);
            } 
        }
        public bool CanWriteAtAnyRelatedNode {
            get {
                return Permissions.Any(x => x.CanWrite == true);
            } 
        }
        public bool CanDecryptAtAnyRelatedNode{ 
            get {
                return Permissions.Any(x => x.CanDecrypt == true);
            } 
        }
        public bool CanAddChildrenAtAnyRelatedNode { 
            get {
                return Permissions.Any(x => x.CanAddChildren == true);
            } 
        }

        public IList<PermissionModel> Permissions { get; set;}
        public PermissionsAggregateModel() => Permissions = new List<PermissionModel>();
    }
}