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
using IAuthorizationService = Settings.Common.Interfaces.IAuthorizationService;

namespace Settings.Controllers.api
{
    [Route("api/applications/")]
    public class ApplicationsController : SettingsApiController
    {
        private readonly ISettingsDbContext _context;
        private readonly Queries _queries;
        private readonly IApplicationService _appService;
        private readonly IAuthorizationService _authorizationService;

        public ApplicationsController(ISettingsDbContext context, 
            Queries queries,  
            IApplicationService appService,
            IAuthorizationService authorizationService)
        {
            this._context = context;
            this._queries = queries;
            this._appService = appService;
            this._authorizationService = authorizationService;
        }

        [HttpGet("{applicationName}")]
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
        public IActionResult Index(string applicationName)
        {
            
            var application = _queries.LoadApplicationAndAllChildrenByName(applicationName);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<HierarchicalModel>), 200)]
        public IActionResult GetAll()
        {
            var rootApps = _authorizationService.GetUserApplications(this.UserId);
            if (rootApps.Any()) {
                return Ok(rootApps);
            } else {
                return Forbid();
            }
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