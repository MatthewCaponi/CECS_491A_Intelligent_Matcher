using DataAccess;
using DataAccess.Repositories;
using Logging;
using Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UserManagement.Models;
using static Models.UserProfileModel;

namespace UserManagement.Services
{
    public static class UserCreationService
    {
        
        public static async Task<int> CreateAccount(UserCreateModel model)
        {
            ILogServiceFactory factory = new LogSeviceFactory();
            factory.AddTarget(TargetType.Text);

            ILogService logger = factory.CreateLogService<UserManager>();

            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();

            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            UserAccountModel userAccount = new UserAccountModel(model.Username, model.Password, model.email);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);

            try
            {
                var userAccountId = await userAccountRepository.CreateUserAccount(userAccount);
                UserProfileModel userProfileModel =
                new UserProfileModel(model.FirstName, model.LastName, DateTime.Parse(model.DateOfBirth),
                model.AccountCreationDate, model.accountType.ToString(), "Active", userAccountId);
                await userProfileRepository.CreateUserProfile(userProfileModel);

                return userAccountId;
            }
            catch (Exception e)
            {
                logger.LogError(new UserLoggingEvent(EventName.UserEvent, "", 0, AccountType.User), e, $"Exception: {e.Message}");
                throw new Exception(e.Message, e.InnerException);
            }   
        }
            
    }
}
