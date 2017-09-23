using Settings.Common.Interfaces;
using Settings.Common.Domain;
using Settings.DataAccess;
using System.Linq;

namespace Settings.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ISettingsDbContext _context;

		public ApplicationService(ISettingsDbContext context)
        {
            _context = context;
        }
        public void AddApplication(Application application, int parentApplicationId)
        {
            var parentApplication = _context.Applications.First(x => x.Id == parentApplicationId);
            var parentApplicationChildren = parentApplication.Children.ToList();
            var parentApplicationHasChildren = parentApplicationChildren.Count > 0;
            
            if(parentApplicationHasChildren)
            {
                var parentRight = parentApplication.RightWeight;

                var descendants = _context.Applications.Where(x => x.RightWeight > parentRight);
                var ancestors = _context.Applications.Where(x => x.LeftWeight > parentRight);

                foreach(var desc in descendants)
                {
                    desc.RightWeight = desc.RightWeight + 2;
                }
                foreach(var ancestor in ancestors)
                {
                    ancestor.LeftWeight = ancestor.LeftWeight + 2;
                }

                var left = parentRight + 1;
                var right = parentRight + 2;

                application.LeftWeight = left;
                application.RightWeight = right;

                _context.AddEntity(application);

                _context.SaveChanges();

            } else
            {
                var parentLeft = parentApplication.LeftWeight;

                var ancestorsOfParentApplication = _context.Applications.Where(x => x.RightWeight > parentLeft);
                var restOfTree = _context.Applications.Where(x => x.LeftWeight > parentLeft);

                foreach(var ancestor in ancestorsOfParentApplication)
                {
                    ancestor.RightWeight = ancestor.RightWeight + 2;
                }
                foreach(var rest in restOfTree)
                {
                    rest.LeftWeight = rest.LeftWeight + 2;
                }

                application.LeftWeight = parentLeft + 1;
                application.RightWeight = parentLeft + 2;

                _context.SaveChanges();
            }
        }
    }
}

