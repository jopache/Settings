using System.Linq;
using Settings.Common.Domain;

namespace Settings.Common.Interfaces
{
    public interface ISettingsDbContext
    {
        IQueryable<Setting> Settings { get; }

        IQueryable<Application> Applications { get; }

        IQueryable<Environment> Environments { get; }
    }
}
