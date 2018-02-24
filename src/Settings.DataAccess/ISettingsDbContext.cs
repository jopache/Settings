using System.Linq;
using Settings.Common.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace Settings.DataAccess
{
    public interface ISettingsDbContext
    {
        IQueryable<Setting> Settings { get; }

        IQueryable<Application> Applications { get; }

        IQueryable<Environment> Environments { get; }

        IQueryable<Permission> Permissions { get; }

        int SaveChanges();

        void AddEntity(object obj);

        IDbContextTransaction BeginTransaction();
    }
}
