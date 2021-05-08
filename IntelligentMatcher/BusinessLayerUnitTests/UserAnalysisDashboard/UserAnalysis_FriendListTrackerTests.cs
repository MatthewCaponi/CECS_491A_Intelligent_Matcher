using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ListingRepositories;
using DataAccess.Repositories.LoginTrackerRepositories;
using DataAccess.Repositories.PageVisitTrackerRepositories;
using DataAccess.Repositories.SearchTrackerRepositories;
using IntelligentMatcher.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserAnalysisManager;
using UserManagement.Services;

namespace BusinessLayerUnitTests.UserAnalysisDashboard
{
    [TestClass]

    public class UserAnalysis_FriendListTrackerTests
    {
        

        [DataTestMethod]
        public async Task TrackFriendNumber_ReturnAvgFriends()
        {
            //arrange
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
            IUserReportsRepo userReportsRepo = new UserReportsRepo(dataGateway, connectionString);
            IUserProfileRepository userProfileRepository = new UserProfileRepository(dataGateway, connectionString);
            IUserProfileService userProfileService = new UserProfileService(userProfileRepository);
            IUserAccountService userAccountService = new UserAccountService(userAccountRepository);
            IValidationService validationService = new ValidationService(userAccountService, userProfileService);
            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo, friendListRepo, friendRequestListRepo, userReportsRepo, validationService);
            UserAccountModel userAccountModel = new UserAccountModel();
            var user1 = await userAccountRepository.CreateAccount(userAccountModel);
            var user2 = await userAccountRepository.CreateAccount(userAccountModel);
            await userInteractionService.CreateFriendshipAsync(user1, user2);

           











        }
    }
}
