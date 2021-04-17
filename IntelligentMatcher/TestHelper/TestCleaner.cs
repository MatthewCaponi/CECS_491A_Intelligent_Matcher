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
            IClaimRepository claimRepository = new ClaimRepository(dataGateway, connectionString);
            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);
            IScopeClaimRepository scopeClaimRepository = new ScopeClaimRepository(dataGateway, connectionString);
            IAssignmentPolicyRepository assignmentPolicyRepository = new AssignmentPolicyRepository(dataGateway, connectionString);
            IAssignmentPolicyPairingRepository assignmentPolicyPairingRepository = new AssignmentPolicyPairingRepository(dataGateway, connectionString);
            IUserScopeClaimRepository userScopeClaimRepository = new UserScopeClaimRepository(dataGateway, connectionString);
            IAccessPolicyRepository accessPolicyRepository = new AccessPolicyRepository(dataGateway, connectionString);
            IAccessPolicyPairingRepository accessPolicyPairingRepository = new AccessPolicyPairingRepository(dataGateway, connectionString);

            var accounts = await userAccountRepository.GetAllAccounts();
            var profiles = await userProfileRepository.GetAllUserProfiles();
            var accountSettings = await userAccountSettingsRepository.GetAllSettings();
            var resources = await resourceRepository.GetAllResources();
            var claims = await claimRepository.GetAllClaims();
            var scopes = await scopeRepository.GetAllScopes();
            var scopeClaims = await scopeClaimRepository.GetAllScopeClaims();
            var assignmentPolicies = await assignmentPolicyRepository.GetAllAssignmentPolicies();
            var assignmentPolicyPairings = await assignmentPolicyPairingRepository.GetAllAssignmentPolicyPairings();
            var userScopeClaims = await userScopeClaimRepository.GetAllUserUserScopeClaims();
            var accessPolicies = await accessPolicyRepository.GetAllAccessPolicies();
            var accesssPolicyPairings = await accessPolicyPairingRepository.GetAllAccessPoliciesPairings();

            if (resources != null)
            {
                var num = resources.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await resourceRepository.DeleteResource(i);
                }
            }

            if (claimRepository != null)
            {
                var num = claims.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await claimRepository.DeleteClaim(i);
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

            if (scopeClaims != null)
            {
                var num = scopeClaims.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await scopeClaimRepository.DeleteScopeClaim(i);
                }
            }
            if (assignmentPolicies != null)
            {
                var num = assignmentPolicies.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await assignmentPolicyRepository.DeleteAssignmentPolicy(i);
                }
            }

            if (assignmentPolicyPairings != null)
            {
                var num = assignmentPolicyPairings.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await assignmentPolicyPairingRepository.DeleteAssignmentPolicyPairing(i);
                }
            }

            if (userScopeClaims != null)
            {
                var num = userScopeClaims.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await userScopeClaimRepository.DeleteUserScopeClaim(i);
                }
            }

            if (accessPolicies != null)
            {
                var num = accessPolicies.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await accessPolicyRepository.DeleteAccessPolicy(i);
                }
            }

            if (accesssPolicyPairings != null)
            {
                var num = accesssPolicyPairings.ToList().Count;
                for (int i = 1; i <= num; ++i)
                {
                    await accessPolicyPairingRepository.DeleteAccessPairingPolicy(i);
                }
            }

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


            await ReseedAsync("UserAccount", 0, connectionString, dataGateway);
            await ReseedAsync("UserProfile", 0, connectionString, dataGateway);
            await ReseedAsync("UserAccountSettings", 0, connectionString, dataGateway);
            await ReseedAsync("Resource", 0, connectionString, dataGateway);
            await ReseedAsync("Claim", 0, connectionString, dataGateway);
            await ReseedAsync("Scope", 0, connectionString, dataGateway);
            await ReseedAsync("ScopeClaim", 0, connectionString, dataGateway);
            await ReseedAsync("AssignmentPolicy", 0, connectionString, dataGateway);
            await ReseedAsync("AssignmentPolicyPairing", 0, connectionString, dataGateway);
            await ReseedAsync("UserScopeClaim", 0, connectionString, dataGateway);
            await ReseedAsync("AccessPolicy", 0, connectionString, dataGateway);
            await ReseedAsync("AccessPolicyPairing", 0, connectionString, dataGateway);
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
