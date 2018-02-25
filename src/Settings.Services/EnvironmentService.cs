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

        public EnvironmentService(ISettingsDbContext context){
            this._context = context;
        }

        // todo: This has been intentionally copy pasted from application controller.  Will be abandoning 
        public Common.Domain.Environment AddEnvironment(Common.Domain.Environment environment, int parentEnvironmentId)
        {
            var parentEnvironment = _context.Environments.First(x => x.Id == parentEnvironmentId);
            var parentEnvExists = parentEnvironment != null;
            environment.ParentId = parentEnvironmentId;
            _context.AddEntity(environment);
            _context.SaveChanges();

            return environment;
        }
    }
}