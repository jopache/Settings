using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Settings.Common.Interfaces;
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
        public IActionResult Add(string name, int parentEnvId)
        {
            var parentEnvironment = _context.Environments.FirstOrDefault(x => x.Id == parentEnvId);
            if(parentEnvironment == null)
            {
                return NotFound();
            }
            var environment = _environmentService.AddEnvironment(new Common.Domain.Environment
            {
                Name = name
            }, parentEnvironment.Id);

            return Ok(new {
                Name = environment.Name,
                Id = environment.Id,
                Children = new ArrayList()
            });
        }
    }
}