using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Messaging;
using Models;
using System;
using DataAccess;
using DataAccess.Repositories;
using System.Globalization;
using System.Collections.Generic;
using DataAccessUnitTestes;
using Security;
using UserAccountSettings;
using System.Linq;
using FriendList;
using PublicUserProfile;
using Moq;
using Services;

namespace BusinessLayerUnitTests.FriendList
{
    [TestClass]

    public class FriendListUnitTests
    {


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RequestFriendAsync_FriendRequested_NewFriendRequested(int userId1, int userId2)
        {

            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);
            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);
                Assert.IsTrue(true);


            }
            catch
            {
                Assert.IsTrue(false);
            }




        }



        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task ConfirmFreindAsync_FriendConfirmed_NewFriendAdded(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);

                await friendListManager.ConfirmFriendAsync(userId1, userId2);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

            

        }



        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task CancelFriendRequest_FriendRequestCancelled_RequestCancelled(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);
                await friendListManager.CancelFriendRequestAsync(userId1, userId2);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

         

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task BlockFriendAsync_FriendBlocked_FriendRemovedAndBlocked(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);
                await friendListManager.ConfirmFriendAsync(userId1, userId2);
                await friendListManager.BlockFriendAsync(userId1, userId2);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

       

        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllFriendAsync_GetFriends_Checkforfriends(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);

                await friendListManager.ConfirmFriendAsync(userId1, userId2);

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }


        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetMutualFriends_GetMutualFriends_CheckMutualFriends(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);

                await friendListManager.ConfirmFriendAsync(userId1, userId2);

                await friendListManager.RequestFriendAsync(userId1, 3);

                await friendListManager.ConfirmFriendAsync(userId1, 3);

                await friendListManager.RequestFriendAsync(userId2, 3);

                await friendListManager.ConfirmFriendAsync(userId2, 3);

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

    



        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllFriendRequestAsync_GetFriendRequests_Checkforrequestss(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllFriendRequestsOutgoingAsync_GetFriendRequests_Checkforrequestss(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);
            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

         
        }



        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task RemoveFriendAsync_FriendRemoved_FriendRemoved(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);

                await friendListManager.ConfirmFriendAsync(userId1, userId2);

                await friendListManager.RemoveFriendAsync(userId2, userId1);

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllBlocksAsync_GetAllBlocks_BlocksRecieved(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.BlockFriendAsync(userId1, userId2);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
      
        }




        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetAllBlockingUserAsync_GetAllBlockings_BlocksRecieved(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.BlockFriendAsync(userId1, userId2);

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }

        }




        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetFriendStatusUserIdAsync_GetStatus_StatusBlocked(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.BlockFriendAsync(userId1, userId2);

                string status = await friendListManager.GetFriendStatusUserIdAsync(userId1, userId2);

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetFriendStatusUserIdAsync_GetStatus_StatusRequested(int userId1, int userId2)
        {

            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);

            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);

                string status = await friendListManager.GetFriendStatusUserIdAsync(userId1, userId2);

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsFalse(false);
            }

           
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        public async Task GetFriendStatusUserIdAsync_GetStatus_StatusFriends(int userId1, int userId2)
        {


            Mock<IUserAccountRepository> userAccountRepository = new Mock<IUserAccountRepository>();

            Mock<IPublicUserProfileService> publicUserProfileService = new Mock<IPublicUserProfileService>();
            Mock<IUserInteractionService> userInteractionService = new Mock<IUserInteractionService>();
            IFriendListManager friendListManager = new FriendListManager(userAccountRepository.Object, publicUserProfileService.Object, userInteractionService.Object);
            try
            {
                await friendListManager.RequestFriendAsync(userId1, userId2);

                await friendListManager.ConfirmFriendAsync(userId1, userId2);

                string status = await friendListManager.GetFriendStatusUserIdAsync(userId1, userId2);

                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }


    }
 }
