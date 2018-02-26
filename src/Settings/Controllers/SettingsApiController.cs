using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Settings.Controllers { 
    public class SettingsApiController : Controller {
        protected string UserId {
            get {
                // todo: can probably centralize this logic as it will be used in more places
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault( x => x.Type == "userId");
                return userIdClaim.Value;
            }
        }
    }
}