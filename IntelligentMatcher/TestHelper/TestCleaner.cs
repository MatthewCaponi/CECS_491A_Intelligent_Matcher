using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            IUserAccountCodeRepository userAccountCodeRepository = new UserAccountCodeRepository(dataGateway, connectionString);
            IUserAccountSettingsRepository userAccountSettingsRepository = new UserAccountSettingRepository(dataGateway, connectionString);
            ILoginAttemptsRepository loginAttemptsRepository = new LoginAttemptsRepository(dataGateway, connectionString);
            IResourceRepository resourceRepository = new ResourceRepository(dataGateway, connectionString);
            IClaimRepository claimRepository = new ClaimRepository(dataGateway, connectionString);
            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);
            IScopeClaimRepository scopeClaimRepository = new ScopeClaimRepository(dataGateway, connectionString);
            IAssignmentPolicyRepository assignmentPolicyRepository = new AssignmentPolicyRepository(dataGateway, connectionString);
            IAssignmentPolicyPairingRepository assignmentPolicyPairingRepository = new AssignmentPolicyPairingRepository(dataGateway, connectionString);
            IUserScopeClaimRepository userScopeClaimRepository = new UserScopeClaimRepository(dataGateway, connectionString);
            IAccessPolicyRepository accessPolicyRepository = new AccessPolicyRepository(dataGateway, connectionString);
            IAccessPolicyPairingRepository accessPolicyPairingRepository = new AccessPolicyPairingRepository(dataGateway, connectionString);
            ITraditionalListingSearchRepository traditionalListingSearchRepository = new TraditionalListingSearchRepository(dataGateway, connectionString);


            var accounts = await userAccountRepository.GetAllAccounts();
            var profiles = await userProfileRepository.GetAllUserProfiles();
            var accountCodes = await userAccountCodeRepository.GetAllUserAccountCodes();
            var accountSettings = await userAccountSettingsRepository.GetAllSettings();
            var loginAttempts = await loginAttemptsRepository.GetAllLoginAttempts();
            var resources = await resourceRepository.GetAllResources();
            var claims = await claimRepository.GetAllClaims();
            var scopes = await scopeRepository.GetAllScopes();
            var scopeClaims = await scopeClaimRepository.GetAllScopeClaims();
            var assignmentPolicies = await assignmentPolicyRepository.GetAllAssignmentPolicies();
            var assignmentPolicyPairings = await assignmentPolicyPairingRepository.GetAllAssignmentPolicyPairings();
            var userScopeClaims = await userScopeClaimRepository.GetAllUserUserScopeClaims();
            var accessPolicies = await accessPolicyRepository.GetAllAccessPolicies();
            var accesssPolicyPairings = await accessPolicyPairingRepository.GetAllAccessPoliciesPairings();
            var listings = await traditionalListingSearchRepository.GetAllListings();

            if (resources != null)
            {
                await DeleteAllFromTable("Resource");
                await ReseedAsync("Resource", 0, connectionString, dataGateway);
            }

            if (claimRepository != null)
            {
                await DeleteAllFromTable("Claim");
                await ReseedAsync("Claim", 0, connectionString, dataGateway);
            }

            if (scopeRepository != null)
            {
                await DeleteAllFromTable("Scope");
                await ReseedAsync("Scope", 0, connectionString, dataGateway);
            }

            if (scopeClaims != null)
            {
                await DeleteAllFromTable("ScopeClaim");
                await ReseedAsync("ScopeClaim", 0, connectionString, dataGateway);
            }
            if (assignmentPolicies != null)
            {
                await DeleteAllFromTable("AssignmentPolicy");
                await ReseedAsync("AssignmentPolicy", 0, connectionString, dataGateway);
            }

            if (assignmentPolicyPairings != null)
            {
                await DeleteAllFromTable("AssignmentPolicyPairing");
            }

            if (userScopeClaims != null)
            {
                await DeleteAllFromTable("UserScopeClaim");
                await ReseedAsync("UserScopeClaim", 0, connectionString, dataGateway);
            }

            if (accessPolicies != null)
            {
                await DeleteAllFromTable("AccessPolicy");
                await ReseedAsync("AccessPolicy", 0, connectionString, dataGateway);
            }

            if (accesssPolicyPairings != null)
            {
                await DeleteAllFromTable("AccessPolicyPairing");
                await ReseedAsync("AssignmentPolicyPairing", 0, connectionString, dataGateway);
                await ReseedAsync("AccessPolicyPairing", 0, connectionString, dataGateway);
            }

            if (loginAttempts != null)
            {
                await DeleteAllFromTable("LoginAttempts");
                await ReseedAsync("LoginAttempts", 0, connectionString, dataGateway);
            }

            if (accounts != null)
            {
                await DeleteAllFromTable("UserProfile");
                await DeleteAllFromTable("UserAccountSettings");
                await DeleteAllFromTable("UserAccountCode");
                await DeleteAllFromTable("UserAccount");

                await ReseedAsync("UserAccount", 0, connectionString, dataGateway);
                await ReseedAsync("UserProfile", 0, connectionString, dataGateway);
                await ReseedAsync("UserAccountCode", 0, connectionString, dataGateway);
                await ReseedAsync("UserAccountSettings", 0, connectionString, dataGateway);

            }
            if(listings != null)
            {
                await DeleteAllFromTable("Listing");
                await ReseedAsync("Listing", 0, connectionString, dataGateway);

            }
        }

        private static async Task ReseedAsync(string tableName, int NEWSEEDNUMBER, IConnectionStringData connectionString,
           IDataGateway dataGateway)
        {
            var storedProcedure = "dbo.Testing_Reseed";

            await dataGateway.Execute(storedProcedure, new
            {
                @TableName = tableName,
                @NEWSEEDNUMBER = NEWSEEDNUMBER
            },
                                         connectionString.SqlConnectionString);
        }

        private static async Task DeleteAllFromTable(string tableName)
        {
            IConnectionStringData connectionString = new ConnectionStringData();
            IDataGateway datagateway = new SQLServerGateway();

            try
            {
                var storedProcedure = "dbo.TestCleaner_Delete_All";
                await datagateway.Execute(storedProcedure, new
                {
                    @tableName = tableName
                },
               connectionString.SqlConnectionString);
            }
            catch (SqlException e)
            {
                Debug.WriteLine("Error deleting table: " + tableName);
            }
        }



    }
}
