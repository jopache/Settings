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
                var appAncestors = appModel.FlattenSelfAndAncestors();
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

        public IEnumerable<HierarchicalModel> GetUserEnvironmentsForApplicationWithId(
            string userId, int applicationId) 
        {
            var currentUserPermissions = GetPermissionsForUserWithId( userId )
                .ToList();
            
            var rootApp = _settingsDbContext.Applications
                .First(x => x.ParentId == null);
            
            var rootAppDescendantsModelFlattenned = _queries.LoadApplicationAndAllChildren(rootApp)
                .FlattenChildren();

            var rootEnv = _settingsDbContext.Environments.First(x => x.ParentId == null);
            var rootEnvDescendantsModelFlattenned = _queries.LoadEnvironmentAndAllChildren(rootEnv)
                .FlattenChildren();

            var idsOfAppAndItsAncestors = rootAppDescendantsModelFlattenned
                .First(x => x.Id == applicationId)
                .GetIdsOfSelfAndAncestors()
                .ToList();

            var userPermissionsForThisAppAndAncenstors = 
                (from appOrAncestorId in idsOfAppAndItsAncestors
                join userPermission in currentUserPermissions
                    on appOrAncestorId equals userPermission.ApplicationId
                select userPermission)
                .ToList();


            //get root env nodes
            var myPermsForThisApplication = 
                            (from env in rootEnvDescendantsModelFlattenned
                            join perm in userPermissionsForThisAppAndAncenstors
                                on env.Id equals perm.EnvironmentId
                            select new {
                                EnvironmentId = env.Id,
                                Permission = perm,
                                Environment = env
                            })
                            .ToList();

            var distictPermissionsEnvironmentIds = myPermsForThisApplication.Select( x => x.EnvironmentId )
                .Distinct()
                .ToList();

            var envAncestorPermissionModel = new List<NodeAncestorPair>();

            foreach (var envIdOnPermissions in distictPermissionsEnvironmentIds) {
                var envModel = rootEnvDescendantsModelFlattenned.First( x => x.Id == envIdOnPermissions);
                var envAncestorIds = envModel.GetAncestorIds().ToList();
                //is a root node
                if (envAncestorIds.Count == 0) {
                    envAncestorPermissionModel.Add(new NodeAncestorPair{
                        NodeId = envIdOnPermissions,
                        AncestorId = null
                    });
                } else {
                    envAncestorPermissionModel.AddRange(
                        envModel.GetAncestorIds()
                            .Select( x => new NodeAncestorPair {
                                NodeId = envIdOnPermissions,
                                AncestorId = x
                            })
                    );
                }
            }
            
            // by joining envAncestorPermissionModel on itself; we find perm-nodes that are 
            // children of others
            var nonRootNodeIds = (from environmentNodePermission in envAncestorPermissionModel
                join environmentAncestorNode in envAncestorPermissionModel
                    on environmentNodePermission.AncestorId
                        equals environmentAncestorNode.NodeId
                select environmentNodePermission.NodeId)
                .Distinct()
                .ToList();

            var rootNodeIds = (myPermsForThisApplication.Select(x => x.EnvironmentId).Distinct().ToList())
                .Except(nonRootNodeIds);

            // var rootNodeIds = (from environmentNodePermission in envAncestorPermissionModel
            //     join environmentAncestorNode in envAncestorPermissionModel
            //         on environmentNodePermission.AncestorId
            //             equals environmentAncestorNode.NodeId into wtfbbq
            //     from environmentAncestorNode in wtfbbq.DefaultIfEmpty()
            //     where environmentAncestorNode == null
            //     select environmentNodePermission.NodeId)
            //     .Distinct()
            //     .ToList();

            var rootEnvNodesWithPermissions = rootNodeIds 
                .Select( rootId => rootEnvDescendantsModelFlattenned
                    .First (envModel  => envModel.Id == rootId))
                .ToList();
                
            var allEnvNodesWithPermissions = rootEnvNodesWithPermissions
                .SelectMany(x => x.FlattenChildren())
                .ToList();

            

            // todo: reorganize
            var appAndAncestorData = rootAppDescendantsModelFlattenned
                .First(x => x.Id == applicationId)
                .FlattenSelfAndAncestors()
                .Select( y => new {
                    ApplicationId = y.Id,
                    Depth = y.Depth
                })
                .ToList();

            var permissionsGroupedByApplication = 
                (from permission in currentUserPermissions
                join app in appAndAncestorData 
                    on permission.ApplicationId equals app.ApplicationId
                orderby app.Depth descending
                select new { 
                    Depth = app.Depth,
                    Permission = permission,
                    ApplicationId = permission.ApplicationId,
                    EnvironmentId = permission.EnvironmentId
                }) 
                .GroupBy(x => x.Depth)
                .ToList();

            foreach (var permissionGrouping in permissionsGroupedByApplication) {
                var allPermissionsForApplication = permissionGrouping.ToList();
                
                foreach (var permissionForApplication in allPermissionsForApplication) {
                    var envNodeForPermission = allEnvNodesWithPermissions
                        .First(env => env.Id == permissionForApplication.EnvironmentId);

                    if (envNodeForPermission.AggregatePermissions == null) {
                        envNodeForPermission.AggregatePermissions = new PermissionsAggregateModel();
                    }
                    if(!envNodeForPermission.AggregatePermissions.Permissions.Any()) {
                        envNodeForPermission.AggregatePermissions.Permissions.Add(new PermissionModel {
                            CanRead = permissionForApplication.Permission.CanReadSettings,
                            CanWrite = permissionForApplication.Permission.CanWriteSettings,
                            CanDecrypt = permissionForApplication.Permission.CanDecryptSetting,
                            CanAddChildren = permissionForApplication.Permission.CanCreateChildEnvironments
                        });
                    }
                }

                foreach (var permissionForApplication in allPermissionsForApplication) {
                    var envNodeForPermission = allEnvNodesWithPermissions
                        .First(env => env.Id == permissionForApplication.EnvironmentId);
                    PropagatePermissionsToChildren(envNodeForPermission, false);
                }
                
        
            }
                //todo optimize this
                //if (allPermissionsForApplication.Count == 1) {
                    
                //}


            //propagate permissions down from each root node. 
            foreach (var rootNode in rootEnvNodesWithPermissions) {
                PropagatePermissionsToChildren(rootNode, true);
            }
            
            //todo find a place to put this, doest seem like it should belong;
            //kill the parent to get rid of cyclical serialization issue
            foreach (var rootNode in rootEnvNodesWithPermissions) {
                rootNode.FlattenChildren()
                .ToList()
                .ForEach( child => { child.Parent = null; });
                rootNode.Parent = null;
            }

            return rootEnvNodesWithPermissions;
        }

        public void PropagatePermissionsToChildren(HierarchicalModel model, 
            bool isRootNodeForPermission) {
                //if (isRootNodeForPermission && model.AggregatePermissions == null) {
                //    throw new InvalidOperationException("root node being calculated can not have null permissions");
                //}

                if (model.AggregatePermissions == null) {
                    model.AggregatePermissions = model.Parent?.AggregatePermissions;
                }

                model.Children.ToList().ForEach(child => {
                    PropagatePermissionsToChildren(child, false);
                });
        }
    } 
}