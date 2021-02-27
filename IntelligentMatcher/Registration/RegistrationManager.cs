using DataAccess;
using DataAccess.Repositories;
using Models;
using System;
using Logging;
using UserManagement.Models;
using UserManagement.Services;
using System.Threading.Tasks;
using static Models.UserProfileModel;

namespace Registration
{
    public class RegistrationManager : IRegistrationManager
    {

        ILogService _logger;

        public RegistrationManager()
        {
            ILogServiceFactory factory = new LogSeviceFactory();
            factory.AddTarget(TargetType.Text);

            _logger = factory.CreateLogService<RegistrationManager>();
        }

        public async Task<int> RegisterNewAccount(UserCreateModel model)
        {
            try
            {
                return await UserCreationService.CreateAccount(model);
            }
            catch (Exception e)
            {
                _logger.LogError(new UserLoggingEvent(EventName.UserEvent, "", 0, AccountType.User), e, $"Exception: {e.Message}");
                throw new Exception(e.Message, e.InnerException);
            }
        }

    }
}
