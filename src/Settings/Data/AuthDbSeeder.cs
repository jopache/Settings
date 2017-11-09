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
                    UserName = "admin",
                    Email = "admin@admin.com"
                };

                var result = await _userManager.CreateAsync(user, "admin");
                if(result.Succeeded) {
                    
                }
            }
        }
    }
}