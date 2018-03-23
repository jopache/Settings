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

                var userPortalApp =  _settingsContext.Applications.First( x => x.Name == "UserPortal");
                var paymentsApiApp = _settingsContext.Applications.First( x => x.Name == "PaymentsApi");
                var dataIntegrationApp = _settingsContext.Applications.First( x => x.Name == "DataIntegration");
                var engineeringApp = _settingsContext.Applications.First( x => x.Name == "Engineering");
                var allEnv = _settingsContext.Environments.First( x => x.Name == "All");
                var productionEnv = _settingsContext.Environments.First( x => x.Name == "Production");
                var stagingEnv = _settingsContext.Environments.First( x => x.Name == "Staging");
                var developmentEnv = _settingsContext.Environments.First( x => x.Name == "Development");
                var developmentJoseEnv = _settingsContext.Environments.First( x => x.Name == "Development-Jose");
                
                //read on userportal-all
                _settingsContext.Permissions.Add(new Permission {
                    UserId = nonAdminUser.Id,
                    CanCreateChildApplications = false,
                    CanCreateChildEnvironments = false,
                    CanDecryptSetting = false,
                    CanReadSettings = true,
                    CanWriteSettings = false,
                    EnvironmentId = allEnv.Id,
                    ApplicationId = userPortalApp.Id
                });
                //read-write - userportal - development
                _settingsContext.Permissions.Add(new Permission {
                    UserId = nonAdminUser.Id,
                    CanCreateChildApplications = false,
                    CanCreateChildEnvironments = false,
                    CanDecryptSetting = false,
                    CanReadSettings = true,
                    CanWriteSettings = true,
                    EnvironmentId = developmentEnv.Id,
                    ApplicationId = userPortalApp.Id
                });
                //read-write-decrypt - userportal - development-jose
                _settingsContext.Permissions.Add(new Permission {
                    UserId = nonAdminUser.Id,
                    CanCreateChildApplications = false,
                    CanCreateChildEnvironments = false,
                    CanDecryptSetting = true,
                    CanReadSettings = true,
                    CanWriteSettings = true,
                    EnvironmentId = developmentJoseEnv.Id,
                    ApplicationId = userPortalApp.Id
                });

                //read-write-decrypt - all - dataintegration
                _settingsContext.Permissions.Add(new Permission {
                    UserId = nonAdminUser.Id,
                    CanCreateChildApplications = false,
                    CanCreateChildEnvironments = false,
                    CanDecryptSetting = true,
                    CanReadSettings = true,
                    CanWriteSettings = true,
                    EnvironmentId = allEnv.Id,
                    ApplicationId = dataIntegrationApp.Id
                });

                //read-write- staging - payments
                _settingsContext.Permissions.Add(new Permission {
                    UserId = nonAdminUser.Id,
                    CanCreateChildApplications = false,
                    CanCreateChildEnvironments = false,
                    CanDecryptSetting = false,
                    CanReadSettings = true,
                    CanWriteSettings = true,
                    EnvironmentId = stagingEnv.Id,
                    ApplicationId = paymentsApiApp.Id
                });

                //read - integration - engin
                _settingsContext.Permissions.Add(new Permission {
                    UserId = nonAdminUser.Id,
                    CanCreateChildApplications = false,
                    CanCreateChildEnvironments = false,
                    CanDecryptSetting = false,
                    CanReadSettings = true,
                    CanWriteSettings = false,
                    EnvironmentId = developmentEnv.Id,
                    ApplicationId = engineeringApp.Id
                });


                _settingsContext.SaveChanges();
            }
        }
    }
}