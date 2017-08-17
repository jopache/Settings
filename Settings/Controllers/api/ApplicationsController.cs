using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Settings.Common.Interfaces;
using Settings.DataAccess;
using Settings.Services;

namespace Settings.Controllers.api
{
    [Route("api/applications/")]
    public class ApplicationsController : Controller
    {

        private readonly ISettingsDbContext _context;
        private readonly Queries _queries;
        private readonly HierarchyHelper _hierarchyHelper;

        public ApplicationsController(ISettingsDbContext context, Queries queries,
            HierarchyHelper hierarchyHelper)
        {
            _context = context;
            _queries = queries;
            _hierarchyHelper = hierarchyHelper;
        }
        [HttpGet("{applicationName}")]
        public IActionResult Index(string applicationName)
        {
            var applications = _queries.GetApplicationAndChildren(applicationName)
                .ToList();
            if (!applications.Any())
            {
                return NotFound();
            }
            var appsTree = _hierarchyHelper.GetHierarchicalTree(applications.First());
            return Ok(appsTree);
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            var applications = _context
                .Applications
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