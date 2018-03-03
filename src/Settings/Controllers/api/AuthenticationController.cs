using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Settings.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System;
using Settings.Data;

namespace Settings.Controllers.api
{
    [Route("api/authentication/")]
    public class AuthenticationController : SettingsApiController {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly AuthDbContext _authDbContext;
    private readonly IAuthorizationService _authorizationService;

    public AuthenticationController(UserManager<User> userManager, 
      SignInManager<User> signInManager,
      AuthDbContext authDbContext,
      IAuthorizationService authorizationService){
        _userManager = userManager;
        _signInManager = signInManager;
        _authDbContext = authDbContext;
        _authorizationService = authorizationService;
    }
       
    [AllowAnonymous]
    // todo: make this a real login
    [HttpPost("login")]
    //[HttpPost]
     public async Task<IActionResult> Login([FromBody]LoginModel model){
         var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
         return Ok();
     }

    [AllowAnonymous]
    [HttpPost("jwt")]
    [ProducesResponseType(typeof(JwtToken), 200)]
    public async Task<IActionResult> GenerateToken([FromBody]LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null)
        {
          
          var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
          if (result.Succeeded)
          {
            // todo: need to see about using Identity claims stuff for storing/reading this stuff.
            //  will eventually be helpful to stick the permissions inside the jwt token rather than 
            // looking them up each time. 
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim("isAdmin", user.IsAdmin.ToString()),
              new Claim("userId",user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a very much longer string that is sure to be longer"));
    

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            

            // todo: drop this hardcoding habbit, it's getting out of hand
            var token = new JwtSecurityToken("test", "test",
              claims,
              expires: DateTime.Now.AddDays(30),
              signingCredentials: creds);

            return Ok(new JwtToken{ Token = new JwtSecurityTokenHandler().WriteToken(token) });
          }
          
        }

        return BadRequest("Could not create token");
      }

    }
}