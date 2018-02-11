using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Settings.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Settings.Controllers.api
{
    [Route("api/authorization/")]
    public class AuthorizationController : Controller{
       private readonly UserManager<User> _userManager;
       private readonly SignInManager<User> _signInManager;
       public AuthorizationController(UserManager<User> userManager, SignInManager<User> signInManager){
           _userManager = userManager;
           _signInManager = signInManager;
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

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim("isAdmin", user.IsAdmin.ToString())
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