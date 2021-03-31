using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UserAccountSettings;
using DataAccess;
using Security;
using Models;
using UserManagement;
using DataAccess.Repositories;

namespace IntelligentMatcherUserInterface.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class UserAccountSettingsController : ControllerBase
    {


        public UserAccountSettingsController()
        {
        }

        [HttpPut]
        [Route("api/UserAccountSettings/ChangeEmail")]
        public bool Edit(string oldPassword, string email, int userId)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepository);
            IAuthenticationService authenticationService = new AuthenticationService(userAccountRepository);
            IAccountSettingsManager userAccountSettingsManager = new AccountSettingsManager(userAccountRepository, userAccountSettingsRepository, cryptographyService, authenticationService);

            CreatedAtAction("Get", userAccountSettingsManager.ChangeEmail(oldPassword, email, userId));

            return true;
        }

  
        public override NoContentResult NoContent()
        {
            return base.NoContent();
        }
    }
}
