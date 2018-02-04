using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Settings.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Settings.Controllers.api
{
    [Route("/api/users/")]
    public class UserController : Controller {
        private readonly UserManager<User> userManager;

        public UserController(UserManager<User> userManager) {
            this.userManager = userManager;
        }
        
        //TODO: Implement proper security, not just auto setting a password
        [Route("add/")]
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> AddEditUser(
            [FromBody]AddEditUserModel addEditUserModel) {
            var user = new User{
                    UserName = addEditUserModel.Username
                };
            
            if (await userManager.FindByNameAsync(addEditUserModel.Username) != null) {
                return BadRequest("User already exists");
            }
        
            var result = await userManager.CreateAsync(user, "admin");
            if(result.Succeeded) {
                //todo: figure out why it needs an object in response
                return Ok(user.Id);
            }
            return BadRequest();
        }

        //TODO: Need to paginate this
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public IActionResult ViewUsers() {
            var listOfUsers = userManager.Users.Select(x => 
                                new { 
                                    Username = x.UserName,
                                    IsAdmin = x.IsAdmin,
                                    Id = x.Id
                                })
                                .ToList();

            return Ok(listOfUsers);
        }
    }
}