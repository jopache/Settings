using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Settings.Common.Domain;
using Settings.Common.Interfaces;
using Settings.Common.Models;
using Settings.DataAccess;
using Settings.Services;
using IAuthorizationService = Settings.Common.Interfaces.IAuthorizationService;

namespace Settings.Controllers.api
{
    [Route("api/environments/")]
    public class EnvironmentsController : SettingsApiController {
        private readonly ISettingsDbContext _context;
        private readonly Queries _queries;
        private readonly IEnvironmentService _environmentService;
        private readonly IAuthorizationService _authorizationService;

        public EnvironmentsController(ISettingsDbContext context, 
            Queries queries, 
            IEnvironmentService environmentService,
            IAuthorizationService authorizationService)
        {
            this._context = context;
            this._queries = queries;
            this._environmentService = environmentService;
            this._authorizationService = authorizationService;
        }

        //todo: project Environment object
        [HttpGet("{environmentName}")]
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult Index(string environmentName)
        {
            var environment = _queries.LoadEnvironmentAndAllChildrenByName(environmentName);
            if (environment == null)
            {
                return NotFound();
            }

            // todo: don't want to return the environment object directlh in case EF Voodoo
            return Ok(environment);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult GetAll()
        {
            var permissions = _authorizationService.GetPermissionsForUserWithId(this.UserId);
            if (permissions.Any()) {
                // todo: need to do more than just get first permission entry here
                var envId = permissions.First().EnvironmentId;
                var env = _context.Environments.First(x => x.Id == envId);
                var model = _queries.LoadEnvironmentAndAllChildren(env);
                return Ok(model);
            } else {
                return Forbid();
            }
        }

        [HttpGet("for-application/{applicationName}")]
        public IActionResult EnvironmentsForApplication(string applicationName) {
            var appId = _context.Applications
                .First(x => x.Name == applicationName)
                .Id;
            var rootEnvsForApp = _authorizationService
                .GetUserEnvironmentsForApplicationWithId(this.UserId, appId);
                
            if (rootEnvsForApp.Any()) {
                return Ok(rootEnvsForApp);
            } else {
                return Forbid();
            }
        }


        [HttpPost("add/parent-{parentEnvId}/new-{name}/")]
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult Add(string name, int parentEnvId)
        {
            var parentEnvironment = _context.Environments.FirstOrDefault(x => x.Id == parentEnvId);
            if(parentEnvironment == null)
            {
                return NotFound();
            }
            var environment = _environmentService.AddEnvironment(new Environment
            {
                Name = name
            }, parentEnvironment.Id);

            return Ok(new HierarchicalModel {
                Name = environment.Name,
                Id = environment.Id,
                Children = new List<HierarchicalModel>()
            });
        }
    }
}