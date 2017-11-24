using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IApplicationService _appService;

        public ApplicationsController(ISettingsDbContext context, Queries queries,
            HierarchyHelper hierarchyHelper, IApplicationService appService)
        {
            _context = context;
            _queries = queries;
            _hierarchyHelper = hierarchyHelper;
            _appService = appService;
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
                .OrderBy(x => x.ParentId)
                .ThenBy(x => x.Id)
                .ToList();
            //todo: query above is not well thought out and since null comes last in postgres responds differently across ms and postgres
            //adding this in for now since postgres but will have to be thought out again. 
            var app = applications.First(x => x.ParentId != null);
            var applicationsTree = _hierarchyHelper.GetHierarchicalTree(app);

            return Ok(applicationsTree);
        }

        [HttpPost("add/parent-application-{parentAppId}/new-application-{applicationName}/")]
        public IActionResult Add(string applicationName, int parentAppId)
        {
            var parentApplication= _context.Applications.FirstOrDefault(x => x.Id == parentAppId);
            if(parentApplication == null)
            {
                return NotFound();
            }
            var application = _appService.AddApplication(new Common.Domain.Application
            {
                Name = applicationName
            }, parentApplication.Id);

            return Ok(new {
                Name = application.Name,
                Id = application.Id,
                Children = new ArrayList()
            });
        }
    }
}