using System.Linq;
using System;
using Settings.Common.Domain;
using System.Collections.Generic;
using Settings.Common.Interfaces;
using Settings.Common.Models;
using Environment = Settings.Common.Domain.Environment;

namespace Settings.DataAccess
{
    // todo: rename this to not queries
    public class Queries
    {
        private ISettingsDbContext Context { get; }

        public Queries(ISettingsDbContext context)
        {
            Context = context;
        }

        public HierarchicalModel LoadApplicationAndAllChildrenByName(string applicationName)
        {
            var application = Context.Applications
                .FirstOrDefault(x => x.Name == applicationName);
            
            if (application == null) 
            {
                return null;
            }

            return LoadApplicationAndAllChildren(application);
        }
        public HierarchicalModel LoadApplicationAndAllChildren(Application app){
            return LoadApplicationAndAllChildren(app, null, 0);
        }

        private HierarchicalModel LoadApplicationAndAllChildren(Application app, HierarchicalModel parent, int depth) {
           var hm = new HierarchicalModel{
               Name = app.Name,
               Id = app.Id,
               ParentId = parent?.Id,
               Parent = parent,
               Children = new List<HierarchicalModel>(),
               Depth = depth
           };

           var childApps = Context.Applications
               .Where(x => x.ParentId == app.Id)
               .ToList();

           foreach(var childApp in childApps) {
               hm.Children.Add(LoadApplicationAndAllChildren(childApp, hm, depth + 1));
           }
           return hm;
        }

        public HierarchicalModel LoadEnvironmentAndAllChildren(Environment env) {
            return LoadEnvironmentAndAllChildren(env, null, 0);
        }

        private HierarchicalModel LoadEnvironmentAndAllChildren(Environment env, HierarchicalModel parent, int depth) {
            var hm = new HierarchicalModel {
                Name = env.Name,
                Id = env.Id,
                ParentId = parent?.Id,
                Parent = parent,
                Children = new List<HierarchicalModel>(),
                Depth = depth
            };

           var childEnvs = Context.Environments
               .Where(x => x.ParentId == env.Id)
               .ToList();

           foreach(var childEnv in childEnvs) {
               hm.Children.Add(LoadEnvironmentAndAllChildren(childEnv, hm, depth + 1));
           }
           return hm;
        }

        public HierarchicalModel LoadEnvironmentAndAllChildrenByName(string environmentName) {
            var environment = Context.Environments
                .FirstOrDefault(x => x.Name == environmentName);

            if (environment == null) {
                return  null;
            }
            return LoadEnvironmentAndAllChildren(environment);
        }

        public HierarchicalModel LoadApplicationAndItsAncestors(Application app) {
            return LoadApplicationAndItsAncestors(app, 0);
        }

        private HierarchicalModel LoadApplicationAndItsAncestors(Application app, int depth) {
            var hm = new HierarchicalModel {
                Name = app.Name,
                Id = app.Id,
                ParentId = app.ParentId,
                Children = new List<HierarchicalModel>(),
                Depth = depth
            };

            if (hm.ParentId.HasValue) {
                var parent = Context.Applications.First(x => x.Id == hm.ParentId);
                hm.Parent = LoadApplicationAndItsAncestors(parent, depth - 1);
            }
            return hm;
        }

        public HierarchicalModel LoadApplicationAndItsAncestorsByName(string applicationName) {
            var application = Context.Applications.FirstOrDefault(x => x.Name == applicationName);
            if (application == null) {
                return null;
            }
            return LoadApplicationAndItsAncestors(application, 0);
        }

        public HierarchicalModel LoadEnvironmentAndItsAncestorsByName(string environmentName) {
            var env = Context.Environments.FirstOrDefault(x => x.Name == environmentName);
            if (env == null) {
                return null;
            }
            return LoadEnvironmentAndItsAncestors(env, 0);
        }

        public HierarchicalModel LoadEnvironmentAndItsAncestors(Environment env, int depth) {
            var hm = new HierarchicalModel {
                Name = env.Name,
                Id = env.Id,
                ParentId = env.ParentId,
                Children = new List<HierarchicalModel>(),
                Depth = depth
            };

            if (hm.ParentId.HasValue) {
                var parent = Context.Environments.First(x => x.Id == hm.ParentId);
                hm.Parent = LoadEnvironmentAndItsAncestors(parent, depth - 1);
            }
            return hm;
        }
    }
}
