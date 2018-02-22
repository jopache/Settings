using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Settings.Common.Interfaces;
using Settings.Common.Models;
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
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult Index(string applicationName)
        {
            var application = _queries.LoadApplicationAndAllChildren(applicationName);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult GetAll()
        {
            // todo: fix once have permissions stuff
            return Index("Global");
        }

        [HttpPost("add/parent-{parentAppId}/new-{name}/")]
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult Add(string name, int parentAppId)
        {
            var parentApplication= _context.Applications.FirstOrDefault(x => x.Id == parentAppId);
            if(parentApplication == null)
            {
                return NotFound();
            }
            var application = _appService.AddApplication(new Common.Domain.Application
            {
                Name = name
            }, parentApplication.Id);

            var response = new HierarchicalModel{
                Name = application.Name,
                Id = application.Id,
                Children = new List<HierarchicalModel>()
            };
            return Ok(response);
        }
    }
}