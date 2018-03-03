using System;
using System.Collections.Generic;
using Settings.Common.Domain;

namespace Settings.Common.Interfaces {
    public interface IAuthorizationService {
        IEnumerable<Permission> GetPermissionsForUserWithId(string userId);
        bool UserCanReadSettings(string userId, string applicationName, string environmentName);
        
    }

}