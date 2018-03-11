using Settings.Common.Domain;
using Settings.Common.Interfaces;
using Settings.Common.Models;
using Settings.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Settings.Services {
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISettingsDbContext _settingsDbContext;
        private readonly Queries _queries;

        public AuthorizationService(ISettingsDbContext settingsDbContext,
            Queries queries)
        {
            this._settingsDbContext = settingsDbContext;
            this._queries = queries;
        }

        public IEnumerable<Permission> GetPermissionsForUserWithId(string userId) {
            return _settingsDbContext.Permissions
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public class AppPermissionsModel {
            public int NodeId;

            public int ParentId;

            public Permission permission;
            public AppPermissionsModel parent;

            public HierarchicalModel model;
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

                var appIdsInHierarchy = applicationHierarchyModel.FlattenChildren();
                var envIdsInHierarchy = envHierarchyModel.FlattenChildren();

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
        
        private class NodeAncestorPair {
            public int NodeId { get; set; }
            public int?  AncestorId { get; set; }
        }

        public IEnumerable<HierarchicalModel> GetUserApplications(string userId){
            var currentUserPermissions = GetPermissionsForUserWithId( userId ).ToList();
            var appIdsSpecifiedOnPermissionsObjects = currentUserPermissions
                                                .Select( x => x.ApplicationId )
                                                .Distinct();
                        
            var rootApp = _settingsDbContext.Applications.First(x => x.ParentId == null);
            var rootAppDescendantsModel = _queries.LoadApplicationAndAllChildren(rootApp);
            var rootAppDescendantsModelFlattenned = rootAppDescendantsModel.FlattenChildren();

            var appAncestorPermissionModel = new List<NodeAncestorPair>();

            foreach(var appId in appIdsSpecifiedOnPermissionsObjects) {
                var appModel = rootAppDescendantsModelFlattenned.First( x => x.Id == appId);
                var appAncestors = appModel.FlattenAncestors();
                var appToAncestorPairs = appModel.GetAncestorIds().Select(x => new NodeAncestorPair {
                    NodeId = appId,
                    AncestorId = x
                });
                appAncestorPermissionModel.AddRange(appToAncestorPairs);
            }

            //if an application's ancestorId is one which we have permissions for
            //then it is not a root, let's get all the non roots. 
            //this logic might break later :-(
            var appIdsOfNonRootPermissions = appAncestorPermissionModel
                .Where( x => appIdsSpecifiedOnPermissionsObjects.Contains(x.AncestorId.Value))
                .Select( x => x.NodeId)
                .ToList();
            
            var rootPermissionAppIds = appIdsSpecifiedOnPermissionsObjects
                .Where( x => !appIdsOfNonRootPermissions.Contains(x));

            var rootNodes = rootPermissionAppIds 
                .Select( rootId => rootAppDescendantsModelFlattenned
                    .First (appModel  => appModel.Id == rootId))
                .ToList();

            // copy permissions to each individual node where it stands
            currentUserPermissions.ForEach( permission => {
                var appNodeForPermission = rootAppDescendantsModelFlattenned
                    .First( x => x.Id == permission.ApplicationId);
                
                if (appNodeForPermission.AggregatePermissions == null) {
                    appNodeForPermission.AggregatePermissions = new PermissionsAggregateModel();
                }
                appNodeForPermission.AggregatePermissions.Permissions.Add(new PermissionModel {
                    CanRead = permission.CanReadSettings,
                    CanWrite = permission.CanWriteSettings,
                    CanAddChildren = permission.CanCreateChildApplications,
                    CanDecrypt = permission.CanDecryptSetting,
                    ApplicationId = permission.ApplicationId,
                    EnvironmentId = permission.EnvironmentId
                });
            });

            //propagate permissions down from each root node. 
            rootNodes.ForEach(rootNode => {
                PropagatePermissionsToChildren(rootNode, true);
            });
            
            //todo find a place to put this, doest seem like it should belong;
            //kill the parent to get rid of cyclical serialization issue
            rootNodes.ForEach(rootNode => {
                rootNode.FlattenChildren()
                    .ToList()
                    .ForEach( child => { child.Parent = null; });
                rootNode.Parent = null;
            });

            return rootNodes;
        }

        public void PropagatePermissionsToChildren(HierarchicalModel model, 
            bool isRootNodeForPermission) {
                if (isRootNodeForPermission && model.AggregatePermissions == null) {
                    throw new InvalidOperationException("root node being calculated can not have null permissions");
                }

                if (model.AggregatePermissions == null) {
                    model.AggregatePermissions = model.Parent.AggregatePermissions;
                }

                model.Children.ToList().ForEach(child => {
                    PropagatePermissionsToChildren(child, false);
                });
        }
    } 
}