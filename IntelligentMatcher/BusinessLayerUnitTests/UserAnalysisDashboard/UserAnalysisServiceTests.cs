using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using DataAccess.Repositories.LoginTrackerRepositories;
using DataAccess.Repositories.PageVisitTrackerRepositories;
using DataAccess.Repositories.SearchTrackerRepositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelper;
using UserAnalysisManager;

namespace BusinessLayerUnitTests.UserAnalysisDashboard
{
    [TestClass]
    public class UserAnalysisServiceTests
    {
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel();
                userAccountModel.Id = i;
                userAccountModel.Username = "TestUser" + i;
                userAccountModel.Password = "TestPassword" + i;
                userAccountModel.Salt = "TestSalt" + i;
                userAccountModel.EmailAddress = "TestEmailAddress" + i;
                userAccountModel.AccountType = "TestAccountType" + i;
                userAccountModel.AccountStatus = "TestAccountStatus";
                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

                await userAccountRepository.CreateAccount(userAccountModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
        }
        [DataTestMethod]
        [DataRow("Suspended")]
        public async Task TrackSuspendedAccounts_Countaccounts(string status)
        {
            int expected = 20;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ILoginTrackerRepo loginTrackerRepo = new LoginTrackerRepo(dataGateway, connectionString);
            IPageVisitTrackerRepo pageVisitTrackerRepo = new PageVisitTrackerRepo(dataGateway, connectionString);
            ISearchTrackerRepo searchTrackerRepo = new SearchTrackerRepo(dataGateway, connectionString);
            IUserAnalysisService userAnalysisService = new UserAnalysisService(friendListRepo, listingRepository, userAccountRepository,
        loginTrackerRepo, pageVisitTrackerRepo, friendRequestListRepo, searchTrackerRepo);
            IEnumerable<UserAccountModel> models = await userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                model.AccountStatus = "Suspended";
                await userAccountRepository.UpdateAccountStatus(model.Id, model.AccountStatus);

               

            }

            int count = await userAnalysisService.GetNumOfSuspendedUsers();
            if (expected == count)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow("Banned")]
        public async Task TrackBannedAccounts_Countaccounts(string status)
        {
            int expected = 20;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ILoginTrackerRepo loginTrackerRepo = new LoginTrackerRepo(dataGateway, connectionString);
            IPageVisitTrackerRepo pageVisitTrackerRepo = new PageVisitTrackerRepo(dataGateway, connectionString);
            ISearchTrackerRepo searchTrackerRepo = new SearchTrackerRepo(dataGateway, connectionString);
            IUserAnalysisService userAnalysisService = new UserAnalysisService(friendListRepo, listingRepository, userAccountRepository,
        loginTrackerRepo, pageVisitTrackerRepo, friendRequestListRepo, searchTrackerRepo);
            IEnumerable<UserAccountModel> models = await userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                model.AccountStatus = "Banned";
                await userAccountRepository.UpdateAccountStatus(model.Id, model.AccountStatus);



            }

            int count = await userAnalysisService.GetNumOfBannedUsers();
            if (expected == count)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }
        [DataTestMethod]
        [DataRow("Shadow-Banned")]
        public async Task TrackShadowBannedAccounts_Countaccounts(string status)
        {
            int expected = 20;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
            IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
            IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
            IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
            ILoginTrackerRepo loginTrackerRepo = new LoginTrackerRepo(dataGateway, connectionString);
            IPageVisitTrackerRepo pageVisitTrackerRepo = new PageVisitTrackerRepo(dataGateway, connectionString);
            ISearchTrackerRepo searchTrackerRepo = new SearchTrackerRepo(dataGateway, connectionString);
            IUserAnalysisService userAnalysisService = new UserAnalysisService(friendListRepo, listingRepository, userAccountRepository,
        loginTrackerRepo, pageVisitTrackerRepo, friendRequestListRepo, searchTrackerRepo);
            IEnumerable<UserAccountModel> models = await userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                model.AccountStatus = "Shadow-Banned";
                await userAccountRepository.UpdateAccountStatus(model.Id, model.AccountStatus);



            }

            int count = await userAnalysisService.GetNumOfShadowBannedUsers();
            if (expected == count)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }
        //[DataTestMethod]
        //[DataRow("Suspended",  "3 / 28 / 2007")]
        //public async Task TrackSuspendedAccountsByDay_Countaccounts(string status,DateTimeOffset dateTimeOffset)
        //{
        //    int expected = 20;
        //    IDataGateway dataGateway = new SQLServerGateway();
        //    IConnectionStringData connectionString = new ConnectionStringData();
        //    IFriendListRepo friendListRepo = new FriendListRepo(dataGateway, connectionString);
        //    IFriendRequestListRepo friendRequestListRepo = new FriendRequestListRepo(dataGateway, connectionString);
        //    IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
        //    IFriendBlockListRepo friendBlockListRepo = new FriendBlockListRepo(dataGateway, connectionString);
        //    IPublicUserProfileRepo publicUserProfileRepo = new PublicUserProfileRepo(dataGateway, connectionString);
        //    IListingRepository listingRepository = new ListingRepository(dataGateway, connectionString);
        //    ILoginTrackerRepo loginTrackerRepo = new LoginTrackerRepo(dataGateway, connectionString);
        //    IPageVisitTrackerRepo pageVisitTrackerRepo = new PageVisitTrackerRepo(dataGateway, connectionString);
        //    ISearchTrackerRepo searchTrackerRepo = new SearchTrackerRepo(dataGateway, connectionString);
        //    IUserAnalysisService userAnalysisService = new UserAnalysisService(friendListRepo, listingRepository, userAccountRepository,
        //loginTrackerRepo, pageVisitTrackerRepo, friendRequestListRepo, searchTrackerRepo);
        //    IEnumerable<UserAccountModel> models = await userAccountRepository.GetAllAccounts();
        //    List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
        //    foreach (var model in models)
        //    {
        //        model.AccountStatus = "Suspended";
        //        model.UpdationDate = ;
        //        //await userAccountRepository.UpdateAccountStatus(model.Id, model.AccountStatus);



        //    }

        //    int count = await userAnalysisService.GetNumOfShadowBannedUsers();
        //    if (expected == count)
        //    {
        //        Assert.IsTrue(true);
        //    }
        //    else
        //    {
        //        Assert.IsTrue(false);
        //    }

        //}










    }//end of class
  

  
}
