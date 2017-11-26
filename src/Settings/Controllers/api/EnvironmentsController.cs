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
        [HttpGet("{environmentName}")]
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult Index(string environmentName)
        {
            var environments = _queries.GetEnvironmentAndChildren(environmentName)
                .ToList();
            if (!environments.Any())
            {
                return NotFound();
            }
            var envsTree = _hierarchyHelper.GetHierarchicalTree(environments.First());
            return Ok(envsTree);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult GetAll()
        {
            var environments = _context
                .Environments
                .Include(x => x.Parent)

                .OrderBy(x => x.ParentId)
                
                .ThenBy(x => x.Id)
                .ToList();

            var environmentsTree = _hierarchyHelper
                .GetHierarchicalTree(environments.First());

            return Ok(environmentsTree);
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