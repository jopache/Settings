using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Settings.Models;

namespace Settings.Data{
    public class AuthDbSeeder{
        private readonly AuthDbContext _context;
        private readonly UserManager<User> _userManager;
        public AuthDbSeeder(AuthDbContext context, UserManager<User> userManager){
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            if(!_context.Users.Any())
            {
                var user = new User{
                    UserName = "administrator",
                    Email = "admin@admin.com"
                };

                var result = await _userManager.CreateAsync(user, "administrator");
                if(result.Succeeded) {
                    user.EmailConfirmed = true;
                    user.IsAdmin = true;
                    await _userManager.UpdateAsync(user);
                }
            }
        }
    }
}