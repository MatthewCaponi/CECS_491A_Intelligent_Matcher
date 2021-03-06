using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserAccountSettings;
using Models;
namespace IntelligentMatcherUserInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class UserAccountSettingsController : ControllerBase
    {

        IAccountSettingsManager accountSettingsManager = new AccountSettingsManager();

        public UserAccountSettingsController()
        {
        }

        [HttpPut]
        [Route("api/UserAccountSettings/ChangeEmail")]
        public int Edit(string oldPassword, string email, string userId)
        {
            return CreatedAtAction("Get", accountSettingsManager.ChangeEmail(oldPassword, email, userId));
        }

  
        public override NoContentResult NoContent()
        {
            return base.NoContent();
        }
    }
}
