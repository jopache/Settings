﻿using System.Collections;
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
        [ProducesResponseType(typeof(HierarchicalModel), 200)]
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
                Children = new List<HierarchicalModel>(),
                LeftWeight = application.LeftWeight,
                RightWeight = application.RightWeight
            };
            return Ok(response);
        }
    }
}