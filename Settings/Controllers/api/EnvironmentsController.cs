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

        public EnvironmentsController(ISettingsDbContext context, Queries queries, 
            HierarchyHelper hierarchyHelper)
        {
            _context = context;
            _queries = queries;
            _hierarchyHelper = hierarchyHelper;
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
            var applications = _context
                .Environments
                .Include(x => x.Parent)
                .Select(x => new
                {
                    Name = x.Name,
                    Id = x.Id,
                    ParentId = x.ParentId,
                    ParentName = x.Parent.Name
                })
                .OrderBy(x => x.ParentId)
                .ThenBy(x => x.Id)
                .ToList();

            return Ok(applications);
        }
    }
}