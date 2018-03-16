using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Settings.Common.Domain;
using Settings.DataAccess;
using Settings.Models;

namespace Settings.Data{
    public class AuthDbSeeder{
        private readonly AuthDbContext _context;
        private readonly UserManager<User> _userManager;

        private readonly SettingsDbContext _settingsContext;
        public AuthDbSeeder(AuthDbContext context, UserManager<User> userManager, SettingsDbContext settingsContext){
            _context = context;
            _userManager = userManager;
            _settingsContext = settingsContext;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            if(!_context.Users.Any())
            {

                var adminUser = new User {
                    UserName = "administrator",
                    Email = "admin@admin.com"
                };

                var nonAdminUser = new User {
                    UserName = "nonadministrator",
                    Email = "nonadministrator@admin.com"
                };

                var adminCreateResult = await _userManager.CreateAsync(adminUser, "administrator");
                if(adminCreateResult.Succeeded) {
                    adminUser.IsAdmin = true;
                    await _userManager.UpdateAsync(adminUser);
                }

                var nonAdminCreateResult = await _userManager.CreateAsync(nonAdminUser, "nonadministrator");
                if(nonAdminCreateResult.Succeeded) {
                    adminUser.IsAdmin = true;
                    await _userManager.UpdateAsync(adminUser);
                }

                // todo: stop hardcoding ids
                _settingsContext.Permissions.Add(new Permission {
                    UserId = adminUser.Id,
                    CanCreateChildApplications = true,
                    CanCreateChildEnvironments = true,
                    CanDecryptSetting = true,
                    CanReadSettings = true,
                    CanWriteSettings = true,
                    EnvironmentId = 1,
                    ApplicationId = 1
                });

                _settingsContext.SaveChanges();

                var appsToGivePermissionsTo = new List<Application>{
                    _settingsContext.Applications.First( x => x.Name == "UserPortal"),
                    _settingsContext.Applications.First( x => x.Name == "PaymentsApi"),
                    _settingsContext.Applications.First( x => x.Name == "DataIntegration"),
                };
                var developmentEnv = _settingsContext.Environments.First( x => x.Name == "Development");
                appsToGivePermissionsTo.ForEach( app => {
                    var permission = new Permission {
                        UserId = nonAdminUser.Id,
                        CanCreateChildApplications = false,
                        CanCreateChildEnvironments = false,
                        CanDecryptSetting = false,
                        CanReadSettings = true,
                        CanWriteSettings = false,
                        EnvironmentId = developmentEnv.Id,
                        ApplicationId = app.Id
                    };

                    //todo no more hardcoding ids
                    if (app.Name == "DataIntegration") {
                        permission.CanWriteSettings = true;
                        permission.CanDecryptSetting = true;
                        permission.CanCreateChildApplications = true;
                        permission.CanCreateChildEnvironments = true;
                        permission.EnvironmentId = 1;
                    }
                    _settingsContext.Permissions.Add(permission);
                });

                _settingsContext.SaveChanges();
            }
        }
    }
}