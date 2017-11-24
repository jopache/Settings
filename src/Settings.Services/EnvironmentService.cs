using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Settings.Common.Domain;
using Settings.Common.Interfaces;
using Settings.DataAccess;

namespace Settings.Services {
    public class EnvironmentService : IEnvironmentService
    {
        private readonly ISettingsDbContext _context;
        private readonly HierarchyHelper _hierarchyHelper;

        public EnvironmentService(ISettingsDbContext context, HierarchyHelper hierarchyHelper){
            this._context = context;
            this._hierarchyHelper = hierarchyHelper;
        }

        // todo: This has been intentionally copy pasted from application controller.  Will be abandoning 
        public Common.Domain.Environment AddEnvironment(Common.Domain.Environment environment, int parentEnvironmentId)
        {
            var parentEnvironment = _context.Environments.First(x => x.Id == parentEnvironmentId);
            var parentEnvExists = parentEnvironment != null;
            var parentEnvAlreadyHasChildren = _context.Applications.Count(x => x.ParentId == parentEnvironmentId) > 0;
            environment.ParentId = parentEnvironmentId;

            if (parentEnvAlreadyHasChildren)
            {
                
                var parentRight = parentEnvironment.RightWeight;

                var descendants = _context.Environments.Where(x => x.RightWeight > parentRight);
                var ancestors = _context.Environments.Where(x => x.LeftWeight > parentRight);

                foreach(var desc in descendants)
                {
                    desc.RightWeight = desc.RightWeight + 2;
                }
                foreach(var ancestor in ancestors)
                {
                    ancestor.LeftWeight = ancestor.LeftWeight + 2;
                }

                var left = parentRight + 1;
                var right = parentRight + 2;

                environment.LeftWeight = left;
                environment.RightWeight = right;

                _context.AddEntity(environment);

                _context.SaveChanges();

            } else
            {
                var parentLeft = parentEnvironment.LeftWeight;

                var ancestorsOfParentApplication = _context.Environments.Where(x => x.RightWeight > parentLeft);
                var restOfTree = _context.Environments.Where(x => x.LeftWeight > parentLeft);

                foreach(var ancestor in ancestorsOfParentApplication)
                {
                    ancestor.RightWeight = ancestor.RightWeight + 2;
                }
                foreach(var rest in restOfTree)
                {
                    rest.LeftWeight = rest.LeftWeight + 2;
                }

                
                environment.LeftWeight = parentLeft + 1;
                environment.RightWeight = parentLeft + 2;

                _context.AddEntity(environment);
                _context.SaveChanges();
            }

            var leftW = 1;
            var appToValidate = _context
                .Applications
                .Include(x => x.Children)
                //todo: Killing mme with the stuff tied to one hierarchy. 
                .First(x => x.ParentId == null);
                
            var isValid = _hierarchyHelper.ValidateWeights(appToValidate, ref leftW);
            
            return environment;
        }
    }
}