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
        
        //TODO: Implement better security
        [Route("add/")]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        public async Task<IActionResult> AddEditUser(
            [FromBody]AddUserModel addEditUserModel) {
            
            // todo: ensure they are admin before allowing create users/admin users
            var user = new User{
                    UserName = addEditUserModel.Username,
                    IsAdmin = addEditUserModel.IsAdmin
                };
            
            if (await userManager.FindByNameAsync(addEditUserModel.Username) != null) {
                return BadRequest(new List<string>(){
                    $"Username {addEditUserModel.Username} not available"
                });
            }
        
            var result = await userManager.CreateAsync(user, addEditUserModel.Password);
            if(result.Succeeded) {
               return NoContent();
            } else {
               return BadRequest(result.Errors.Select(x => x.Description));
            }
            
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