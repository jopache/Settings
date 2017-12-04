using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Settings.Models;

namespace Settings.Controllers.api {
    [Route("/api/users/")]
    public class UserController : Controller {
        private readonly UserManager<User> userManager;

        public UserController(UserManager<User> userManager) {
            this.userManager = userManager;
        }
        
        [Route("add-edit/")]
        [HttpPost]
        public IActionResult AddEditUser([FromBody]AddEditUserModel addEditUserModel) {
            //TODO: Implement this.
            return Ok();
        }
    }
}