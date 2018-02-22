using Settings.Common.Interfaces;
using Settings.Common.Domain;
using Settings.DataAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Settings.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ISettingsDbContext _context;

		public ApplicationService(ISettingsDbContext context, HierarchyHelper hierarchyHelper)
        {
            _context = context;
            _hierarchyHelper = hierarchyHelper;
        }

        private readonly HierarchyHelper _hierarchyHelper;

        public Application AddApplication(Application application, int parentApplicationId)
        {
            var parentApplication = _context.Applications.First(x => x.Id == parentApplicationId);
            var parentAppExists = parentApplication != null;
            application.ParentId = parentApplicationId;

            _context.AddEntity(application);
            _context.SaveChanges();

            return application;
        }
    }
}

