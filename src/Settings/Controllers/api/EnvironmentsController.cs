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

namespace Settings.Controllers.api
{
    [Route("api/environments/")]
    public class EnvironmentsController : Controller {

        private readonly ISettingsDbContext _context;
        private readonly Queries _queries;
        private readonly HierarchyHelper _hierarchyHelper;
        private readonly IEnvironmentService _environmentService;

        public EnvironmentsController(ISettingsDbContext context, Queries queries, 
            HierarchyHelper hierarchyHelper, IEnvironmentService environmentService)
        {
            _context = context;
            _queries = queries;
            _hierarchyHelper = hierarchyHelper;
            _environmentService = environmentService;
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
            // todo: hack until I get permissions stuff working
            return Index("All");
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