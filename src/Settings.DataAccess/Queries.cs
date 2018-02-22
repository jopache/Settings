using System.Linq;
using System;
using Settings.Common.Domain;
using System.Collections.Generic;
using Settings.Common.Interfaces;
using Settings.Common.Models;

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

        public HierarchicalModel LoadApplicationAndAllChildren(string applicationName)
        {
            var application = Context.Applications
                .FirstOrDefault(x => x.Name == applicationName);
            
            if (application == null) 
            {
                return null;
            }

            // todo: fix
            return LoadApplicationAndAllChildren(application);
        }

        public HierarchicalModel LoadApplicationAndAllChildren(Application app) {

           var hm = new HierarchicalModel{
               Name = app.Name,
               Id = app.Id,
               ParentId = app.ParentId,
               Children = new List<HierarchicalModel>()
           };

           var childApps = Context.Applications
               .Where(x => x.ParentId == app.Id)
               .ToList();

           foreach(var childApp in childApps) {
               hm.Children.Add(LoadApplicationAndAllChildren(childApp));
           }
           return hm;
        }

        public HierarchicalModel LoadEnvironmentAndAllChildren(
            Settings.Common.Domain.Environment env) {
            var hm = new HierarchicalModel {
                Name = env.Name,
                Id = env.Id,
                ParentId = env.ParentId,
                Children = new List<HierarchicalModel>()
            };

           var childEnvs = Context.Environments
               .Where(x => x.ParentId == env.Id)
               .ToList();

           foreach(var childEnv in childEnvs) {
               hm.Children.Add(LoadEnvironmentAndAllChildren(childEnv));
           }
           return hm;
        }

        public HierarchicalModel LoadEnvironmentAndAllChildren(string environmentName)
        {
            var environment = Context.Environments
                .FirstOrDefault(x => x.Name == environmentName);

            if (environment == null) {
                return  null;
            }
            return LoadEnvironmentAndAllChildren(environment);
        }
    }
}
