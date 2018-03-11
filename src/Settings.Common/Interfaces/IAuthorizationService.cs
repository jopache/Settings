using System;
using System.Collections.Generic;
using Settings.Common.Domain;
using Settings.Common.Models;

namespace Settings.Common.Interfaces {
    public interface IAuthorizationService {
        IEnumerable<Permission> GetPermissionsForUserWithId(string userId);
        bool UserCanReadSettings(string userId, string applicationName, string environmentName);
        IEnumerable<HierarchicalModel> GetUserApplications(string userId);
    }

}