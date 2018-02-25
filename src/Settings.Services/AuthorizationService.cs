using Settings.Common.Domain;
using Settings.Common.Interfaces;
using Settings.Common.Models;
using Settings.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Settings.Services {
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISettingsDbContext _settingsDbContext;
        private readonly Queries _queries;

        public AuthorizationService(ISettingsDbContext settingsDbContext, Queries queries)
        {
            this._settingsDbContext = settingsDbContext;
            this._queries = queries;
        }

        public IEnumerable<Permission> GetPermissionsForUserWithId(string userId) {
            return _settingsDbContext.Permissions
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public bool UserCanReadSettings(string userId, string applicationName, string environmentName) {
            var userPermissions = GetPermissionsForUserWithId(userId);
            if (!userPermissions.Any()) {
                return false;
            }
            var userCanReadSettings = false;
            foreach(var permission in userPermissions) {
                if (!permission.CanReadSettings) {
                    continue;
                }
                var application = _settingsDbContext.Applications
                    .FirstOrDefault(app => app.Id == permission.ApplicationId);
                var environment = _settingsDbContext.Environments
                    .FirstOrDefault(env => env.Id == permission.EnvironmentId);
                
                if(application == null || environment == null) {
                    continue;
                }
                var applicationHierarchyModel = _queries.LoadApplicationAndAllChildren(application);
                var envHierarchyModel = _queries.LoadEnvironmentAndAllChildren(environment);

                var appIdsInHierarchy = HierarchicalModel.FlattenChildren(applicationHierarchyModel);
                var envIdsInHierarchy = HierarchicalModel.FlattenChildren(envHierarchyModel);

                // todo: don't like the way im doing this by name
                var appIncludedInPermission = appIdsInHierarchy.Any(x => x.Name == applicationName);
                var envIncludedInPermission = envIdsInHierarchy.Any(x => x.Name == environmentName);

                if(appIncludedInPermission 
                    && envIncludedInPermission) {
                    userCanReadSettings = true;
                    break;
                }
            }
            return userCanReadSettings;
        }
    }
}