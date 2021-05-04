using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Models;
using DataAccess;
using DataAccess.Repositories;
using System.Globalization;
using DataAccessUnitTestes;
using Security;
using UserAccountSettings;
using System.Linq;
using FriendList;
using PublicUserProfile;
using Services;
using UserManagement.Services;
using IntelligentMatcher.Services;
using Moq;
namespace BusinessLayerUnitTests.Services
{
    [TestClass]

    public class UserIneractionUnitTests
    {


        [DataTestMethod]
        [DataRow(1, 2, "I am creating a report")]
        public async Task CreateReportAsync_CreateReport_ReportCreated(int userId1, int userId2, string report)
        {


            Mock<IFriendRequestListRepo>  friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );






            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);


            UserReportsModel model = new UserReportsModel();
            model.ReportedId = userId1;
            model.ReportingId = userId2;
            model.Report = report;
            try
            {


                await userInteractionService.CreateReportAsync(model);
                Assert.IsTrue(true);
 
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task CreateFriendshipAsync_CreateFriendship_FriendshipCreated(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );



            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);


            try
            {


                await userInteractionService.CreateFriendshipAsync(userId1, userId2);

            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllUserFriendRequests_CreatRequest_RequestCreated(int userId1, int userId2)
        {


            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);


            try
            {


                await userInteractionService.CreateFriendRequestAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllUserFriendRequests(userId1);

           
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllUserFriendRequestsOutgoing_CreatRequest_RequestCreated(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);


            try
            {
                await userInteractionService.CreateFriendRequestAsync(userId2, userId1);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllUserFriendRequestsOutgoing(userId1);

            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task CreateFriendRequestAsync_CreatRequest_RequestCreated(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);


            try
            {


                await userInteractionService.CreateFriendRequestAsync(userId1, userId2);

      
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllUserFriends_CreatRequest_RequestCreated(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);



            try
            {
                await userInteractionService.CreateFriendshipAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllUserFriends(userId2);

               
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task CreateBlockAsync_CreatBlock_BlockCreated(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>();
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>();
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>();
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);


            try
            {


                await userInteractionService.CreateBlockAsync(userId1, userId2);

              
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllUserFriendBlocks_CreatBlock_BlockCreated(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);



            try
            {


                await userInteractionService.CreateBlockAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllUserFriendBlocks(userId1);

            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllBlockingUsers_CreatBlock_BlockCreated(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);



            try
            {


                await userInteractionService.CreateBlockAsync(userId1, userId2);

                IEnumerable<FriendsListJunctionModel> friendsListJunctionModels = await userInteractionService.GetAllBlockingUsers(userId1);

              
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }



        [DataTestMethod]
        [DataRow(1, 2, "I am creating a report")]
        public async Task RemoveReportAsync_RemoveReport_ReportRemoved(int userId1, int userId2, string report)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);



            UserReportsModel model = new UserReportsModel();
            model.ReportedId = userId1;
            model.ReportingId = userId2;
            model.Report = report;
            try
            {


                await userInteractionService.CreateReportAsync(model);

                await userInteractionService.RemoveReportAsync(1);

          
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RemoveFriendAsync_RemoveFriend_FriendshipRemoved(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);


            try
            {


                await userInteractionService.CreateFriendshipAsync(userId1, userId2);
                await userInteractionService.RemoveFriendAsync(userId1, userId2);


            }
            catch
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RemoveFriendRequestAsync_RemoveRequest_RequestRemoved(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);


            try
            {


                await userInteractionService.CreateFriendRequestAsync(userId1, userId2);

                await userInteractionService.RemoveFriendRequestAsync(userId1, userId2);


               
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RemoveFriendBlockAsync_RemoveBlock_BlockRemoved(int userId1, int userId2)
        {
            Mock<IDataGateway> dataGateway = new Mock<IDataGateway>();
            Mock<IConnectionStringData> connectionString = new Mock<IConnectionStringData>();
            Mock<IFriendRequestListRepo> friendRequestListRepo = new Mock<IFriendRequestListRepo>( );
            Mock<IFriendBlockListRepo> friendBlockListRepo = new Mock<IFriendBlockListRepo>( );
            Mock<IFriendListRepo> friendListRepo = new Mock<IFriendListRepo>( );
            Mock<IUserReportsRepo> userReportsRepo = new Mock<IUserReportsRepo>( );
            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>( );

            Mock<IUserProfileRepository> userProfileRepository = new Mock<IUserProfileRepository>( );
            Mock<IUserProfileService> userProfileService = new Mock<IUserProfileService>();
            Mock<IUserAccountService> userAccountService = new Mock<IUserAccountService>();

            Mock<IValidationService> validationService = new Mock<IValidationService>();

            IUserInteractionService userInteractionService = new UserInteractionService(friendBlockListRepo.Object, friendListRepo.Object, friendRequestListRepo.Object, userReportsRepo.Object, validationService.Object);



            try
            {


                await userInteractionService.CreateBlockAsync(userId1, userId2);

                await userInteractionService.RemoveFriendBlockAsync(userId1, userId2);


            }
            catch
            {
                Assert.IsTrue(false);
            }

        }


    }
}
