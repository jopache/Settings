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

                var feUser = new User {
                    UserName = "feuser",
                    Email = "feuser@admin.com"
                };

                var adminCreateResult = await _userManager.CreateAsync(adminUser, "administrator");
                if(adminCreateResult.Succeeded) {
                    adminUser.IsAdmin = true;
                    await _userManager.UpdateAsync(adminUser);
                }

                var feuserCreateResult = await _userManager.CreateAsync(feUser, "feuser");

                // todo: stop hardcoding ids
                // administrator user gets permission at root level. 
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

                var app_frontend =  _settingsContext.Applications.First( x => x.Name == "Frontend");
                var app_backend = _settingsContext.Applications.First( x => x.Name == "Backend");
                var app_igaming = _settingsContext.Applications.First( x => x.Name == "iGaming");
                var app_native = _settingsContext.Applications.First( x => x.Name == "Native");
                var app_twinspires = _settingsContext.Applications.First (x => x.Name == "TwinSpires");
                var app_adw = _settingsContext.Applications.First( x => x.Name == "ADW");

                var env_all = _settingsContext.Environments.First( x => x.Name == "All");
                var env_prod = _settingsContext.Environments.First( x => x.Name == "Production");
                var env_staging = _settingsContext.Environments.First( x => x.Name == "Staging");
                var env_development = _settingsContext.Environments.First( x => x.Name == "Development");
                var env_development_2800 = _settingsContext.Environments.First(x => x.Name == "Dev-2800");
                var env_development_2800_local_jose = _settingsContext.Environments.First( x => x.Name == "Dev-2800-local-jose-pacheco");
                
                

                //igaming permissions
                
                // feuser - igaming - all - read
                AddPermission(feUser.Id, app_igaming.Id, env_all.Id, true, false, false);
                // feuser - igaming - development - read/write/decrypt
                AddPermission(feUser.Id, app_igaming.Id, env_development.Id, true, true, true);
                // feuser - igaming - staging - read/write
                AddPermission(feUser.Id, app_igaming.Id, env_staging.Id, true, true, false);
                
                //twinspires permissions
                // feuser - twinspires - development - read/write/decrypt
                AddPermission(feUser.Id, app_twinspires.Id, env_development.Id, true, true, true);
                // feuser - twinspires - staging - read/write
                AddPermission(feUser.Id, app_twinspires.Id, env_staging.Id, true, true, false);
                // feuser - twinspires - production - read
                AddPermission(feUser.Id, app_twinspires.Id, env_prod.Id, true, false, false);

                // feuser - native - all - read/write
                AddPermission(feUser.Id, app_native.Id, env_all.Id, true, true, true);

                // feuser - adw
                AddPermission(feUser.Id, app_adw.Id, env_development_2800_local_jose.Id, true, true, true);


                _settingsContext.SaveChanges();
            }


        }

        protected void AddPermission(string userId, int appId, int envId, bool read, bool write, bool decrypt) {
            _settingsContext.Permissions.Add(new Permission {
                    UserId = userId,
                    CanCreateChildApplications = false,
                    CanCreateChildEnvironments = false,
                    CanDecryptSetting = decrypt,
                    CanReadSettings = read,
                    CanWriteSettings = write,
                    EnvironmentId = envId,
                    ApplicationId = appId
                });

        }
    }
}