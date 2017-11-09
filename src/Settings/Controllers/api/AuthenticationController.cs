using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Settings.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Settings.Controllers.api
{
    [Route("api/authentication/")]
    public class AuthenticationController : Controller{
       private readonly UserManager<User> _userManager;
       private readonly SignInManager<User> _signInManager;
       public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager){
           _userManager = userManager;
           _signInManager = signInManager;
       }
       
       [HttpGet("login")]
       //[HttpPost]
       [AllowAnonymous]
        public async Task<IActionResult> Login(){
            var result = await _signInManager.PasswordSignInAsync("admin", "admin", true, false);
            
            return Ok();
        }
    }
}