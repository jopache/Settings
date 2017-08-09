using System.Linq;
using Microsoft.EntityFrameworkCore;
using Settings.Common.Domain;
using Settings.Common.Interfaces;

namespace Settings.DataAccess
{
    public class SettingsDbContext : DbContext, ISettingsDbContext
    {
        IQueryable<Setting> ISettingsDbContext.Settings => Settings;
        IQueryable<Application> ISettingsDbContext.Applications => Applications;
        IQueryable<Environment> ISettingsDbContext.Environments => Environments;

        public SettingsDbContext(DbContextOptions<SettingsDbContext> options) : base(options)
        {
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Environment> Environments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Figure out if I have to map all relationships this way or just two way kind
            modelBuilder.Entity<Application>()
                .HasOne<Application>(x => x.Parent)
                .WithMany(x => x.Children);

            modelBuilder.Entity<Environment>()
                .HasOne<Environment>(x => x.Parent)
                .WithMany(x => x.Children);
        }
    }
}
