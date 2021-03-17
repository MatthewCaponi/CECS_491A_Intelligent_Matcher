using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAccountSettings;

namespace WebApi
{
    public class Seed
    {
        public async Task SeedUsers(int seedAmount)
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);


            var accounts = await userAccountRepository.GetAllAccounts();            
            var profiles = await userProfileRepository.GetAllUserProfiles();
            var accountSettings = await userAccountSettingsRepository.GetAllSettings();

            if (accounts != null)
            {
                var numRows = accounts.ToList().Count;

                for (int i = 1; i <= numRows; ++i)
                {                 
                    await userProfileRepository.DeleteUserProfileByAccountId(i);
                    await userAccountSettingsRepository.DeleteUserAccountSettingsByUserId(i);
                    await userAccountRepository.DeleteAccountById(i);
                }
            }
            
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserProfile", 0, connectionString, dataGateway);
            await DataAccessTestHelper.ReseedAsync("UserAccountSettings", 0, connectionString, dataGateway);


            for (int i = 1; i < seedAmount; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel();
                UserProfileModel userProfileModel = new UserProfileModel();
                UserAccountSettingsModel userAccountSettingsModel = new UserAccountSettingsModel();

                userAccountModel.Id = i;
                userAccountModel.Username = "TestUser" + i;
                userAccountModel.Password = "TestPassword" + i;
                userAccountModel.Salt = "TestSalt" + i;
                userAccountModel.EmailAddress = "TestEmailAddress" + i;
                userAccountModel.AccountType = "TestAccountType" + i;
                userAccountModel.AccountStatus = "TestAccountStatus" + i;
                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;
                
                userProfileModel.Id = i;
                userProfileModel.FirstName = "TestFirstName" + i;
                userProfileModel.Surname = "TestSurname" + i;
                userProfileModel.DateOfBirth = DateTimeOffset.UtcNow;
                userProfileModel.UserAccountId = userAccountModel.Id;
                
                userAccountSettingsModel.Id = i;
                userAccountSettingsModel.UserId = userAccountModel.Id;
                userAccountSettingsModel.FontSize = 12;
                userAccountSettingsModel.FontStyle = "Time New Roman";
                userAccountSettingsModel.ThemeColor = "White";
           
                await userAccountRepository.CreateAccount(userAccountModel);        
                await userProfileRepository.CreateUserProfile(userProfileModel);
                await userAccountSettingsRepository.CreateUserAccountSettings(userAccountSettingsModel);
            }
        }
    }
}
