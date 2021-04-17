using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper
{
    public class TestCleaner
    {
        public static async Task CleanDatabase()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            IResourceRepository resourceRepository = new ResourceRepository(dataGateway, connectionString);
            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);

            var accounts = await userAccountRepository.GetAllAccounts();
            var profiles = await userProfileRepository.GetAllUserProfiles();
            var accountSettings = await userAccountSettingsRepository.GetAllSettings();
            var resources = await resourceRepository.GetAllResources();
            var scopes = await scopeRepository.GetAllScopes();


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

            if (resources != null)
            {
                var num = resources.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await resourceRepository.DeleteResource(i);
                }
            }
            
            if (scopeRepository != null)
            {
                var num = scopes.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await scopeRepository.DeleteScope(i);
                }
            }

            await ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await ReseedAsync("UserProfile", 0, connectionString, dataGateway);
            await ReseedAsync("UserAccountSettings", 0, connectionString, dataGateway);
            await ReseedAsync("Resource", 0, connectionString, dataGateway);
            await ReseedAsync("Scope", 0, connectionString, dataGateway);
        }

        private static async Task ReseedAsync(string tableName, int NEWSEEDNUMBER, IConnectionStringData connectionString,
           IDataGateway dataGateway)
        {
            var storedProcedure = "dbo.Testing_Reseed";

            await dataGateway.Execute(storedProcedure, new
            {
                @tableName = tableName,
                @NEWSEEDNUMBER = NEWSEEDNUMBER
            },
                                         connectionString.SqlConnectionString);
        }



    }
}
