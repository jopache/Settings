using System.Linq;
using Settings.Common.Domain;
using Settings.Common.Interfaces;

namespace Settings.DataAccess
{
    public class Queries
    {
        private ISettingsDbContext Context { get; }

        public Queries(ISettingsDbContext context)
        {
            Context = context;
        }

        public IQueryable<Application> GetApplicationAndChildren(string applicationName)
        {
            var application = Context.Applications
                .FirstOrDefault(x => x.Name == applicationName);
            
            //TODO: How should I handle if requested application isn't found?
            //returning the above as queryable seems silly since I already know nothing is there
            //returning a new list that's empty seems silly as well. 

            return (from app in Context.Applications
                where app.LeftWeight >= application.LeftWeight
                      && app.RightWeight <= application.RightWeight
                orderby app.LeftWeight
                    select app);
        }

        public IQueryable<Environment> GetEnvironmentAndChildren(string environmentName)
        {
            var environment = Context.Environments
                .FirstOrDefault(x => x.Name == environmentName);

            //TODO: How should I handle if requested application isn't found?
            //returning the above as queryable seems silly since I already know nothing is there
            //returning a new list that's empty seems silly as well. 

            return (from env in Context.Environments
                where env.LeftWeight >= environment.LeftWeight
                      && env.RightWeight <= environment.RightWeight
                orderby env.LeftWeight
                select env);
        }
    }
}
