using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Services;

namespace FriendList
{
    public class FriendListManager : IFriendListManager
    {

        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IPublicUserProfileService _publicUserProfileService;
        private readonly IUserInteractionService _userInteractionService;

        public FriendListManager(IUserAccountRepository userAccountRepository, IPublicUserProfileService publicUserProfileService, IUserInteractionService userInteractionService)
        {
            _publicUserProfileService = publicUserProfileService;
            _userAccountRepository = userAccountRepository;
            _userInteractionService = userInteractionService;
        }


        public async Task<string> GetFriendStatusUserIdAsync(int userId1, int userId2)
        {
            IEnumerable <FriendsListJunctionModel>  models = await _userInteractionService.GetAllUserFriends(userId1);

            foreach(var model in models)
            {
                if(model.User1Id == userId2 || model.User2Id == userId2)
                {
                    return "Friends";
                }
            }



            models = await _userInteractionService.GetAllBlockingUsers(userId1);

            foreach (var model in models)
            {
                if (model.User1Id == userId2 || model.User2Id == userId2)
                {
                    return "Blocked";
                }
            }
            models = await _userInteractionService.GetAllBlockingUsers(userId2);

            foreach (var model in models)
            {
                if (model.User1Id == userId1 || model.User2Id == userId1)
                {
                    return "Blocked";
                }
            }

            models = await _userInteractionService.GetAllUserFriendRequests(userId1);

            foreach (var model in models)
            {
                if (model.User1Id == userId2 || model.User2Id == userId2)
                {
                    return "Requested";
                }
            }
            models = await _userInteractionService.GetAllUserFriendRequests(userId2);

            foreach (var model in models)
            {
                if (model.User1Id == userId1 || model.User2Id == userId1)
                {
                    return "Requested";
                }
            }
            return "None";



        }

        public async Task<bool> BlockFriendAsync(int userId, int blockedUserId)
        {
            await _userInteractionService.RemoveFriendRequestAsync(userId, blockedUserId);
            await RemoveFriendAsync(userId, blockedUserId);
            FriendsListJunctionModel model = new FriendsListJunctionModel();
            return await _userInteractionService.CreateBlockAsync(userId, blockedUserId);
        }

        public async Task<bool> ConfirmFriendAsync(int requetUserId, int requestedUserId)
        {
            await _userInteractionService.RemoveFriendRequestAsync(requetUserId, requestedUserId);
            FriendsListJunctionModel model = new FriendsListJunctionModel();
            return await _userInteractionService.CreateFriendshipAsync(requetUserId, requestedUserId);
            

        }



        public async Task<IEnumerable<FriendListModel>> GetMutualFriends(int userId1, int userId2)
        {

            IEnumerable<FriendsListJunctionModel> junctionModel1 = await _userInteractionService.GetAllUserFriends(userId1);



            IEnumerable<FriendsListJunctionModel> junctionModel2 = await _userInteractionService.GetAllUserFriends(userId2);

            List<FriendsListJunctionModel> junctionModel = new List<FriendsListJunctionModel>();


            foreach (var user1 in junctionModel1)
            {
                foreach(var user2 in junctionModel2)
                {
                    if(user1.User2Id == user2.User2Id)
                    {
                        junctionModel.Add(user1);
                    }
                    if (user1.User1Id == user2.User2Id)
                    {
                        junctionModel.Add(user1);

                    }
                    if (user1.User2Id == user2.User1Id)
                    {
                        junctionModel.Add(user1);

                    }
                }
            }

            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (var junction in junctionModel)
            {
                if ((junction.User1Id != userId1 && junction.User2Id != userId2) || (junction.User2Id != userId1 && junction.User1Id != userId2))
                {
                    FriendListModel model = new FriendListModel();
                    model.userId = junction.User2Id;


                    UserAccountModel userAccount = await _userAccountRepository.GetAccountById(model.userId);
                    PublicUserProfileModel publicUserProfileModel = await _publicUserProfileService.GetUserProfileAsync(model.userId);
                    model.userProfileImage = publicUserProfileModel.Photo;
                    model.username = userAccount.Username;
                    model.date = DateTime.UtcNow;
                    model.status = publicUserProfileModel.Status;

                    if (model.userId != userId1 && model.userId != userId2)
                    {
                        friendListModel.Add(model);

                    }
                }

               
            }
 
            return friendListModel;

        }




        public async Task<IEnumerable<FriendListModel>> GetAllFriendAsync(int userId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _userInteractionService.GetAllUserFriends(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (var junction in junctionModel)
            {
                FriendListModel model = new FriendListModel();
                if (userId == junction.User1Id)
                {
                    model.userId = junction.User2Id;
                }
                else
                {
                    model.userId = junction.User1Id;
                }

                UserAccountModel userAccount = await _userAccountRepository.GetAccountById(model.userId);
                PublicUserProfileModel publicUserProfileModel = await _publicUserProfileService.GetUserProfileAsync(model.userId);
                model.userProfileImage = publicUserProfileModel.Photo;
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;
                model.status = publicUserProfileModel.Status;
                friendListModel.Add(model);
            }

            return friendListModel;

        }
        public async Task<IEnumerable<FriendListModel>> GetAllBlocksAsync(int userId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _userInteractionService.GetAllBlockingUsers(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (var junction in junctionModel)
            {
                FriendListModel model = new FriendListModel();
                if (userId == junction.User1Id)
                {
                    model.userId = junction.User2Id;
                }
                else
                {
                    model.userId = junction.User1Id;
                }

                UserAccountModel userAccount = await _userAccountRepository.GetAccountById(model.userId);
                PublicUserProfileModel publicUserProfileModel = await _publicUserProfileService.GetUserProfileAsync(model.userId);
                model.userProfileImage = publicUserProfileModel.Photo;
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;
                model.status = publicUserProfileModel.Status;

                friendListModel.Add(model);
            }

            return friendListModel;



        }

        public async Task<IEnumerable<FriendListModel>> GetAllBlockingUserAsync(int userId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _userInteractionService.GetAllBlockingUsers(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (var junction in junctionModel)
            {
                FriendListModel model = new FriendListModel();
                if (userId == junction.User1Id)
                {
                    model.userId = junction.User2Id;
                }
                else
                {
                    model.userId = junction.User1Id;
                }
                PublicUserProfileModel publicUserProfileModel = await _publicUserProfileService.GetUserProfileAsync(model.userId);
                model.userProfileImage = publicUserProfileModel.Photo;
                UserAccountModel userAccount = await _userAccountRepository.GetAccountById(model.userId);
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;
                model.status = publicUserProfileModel.Status;

                friendListModel.Add(model);
            }

            return friendListModel;



        }

        public async Task<IEnumerable<FriendListModel>> GetAllRequestsAsync(int userId)
        {
            IEnumerable< FriendsListJunctionModel> junctionModel = await _userInteractionService.GetAllUserFriendRequests(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (var junction in junctionModel)
            {

                FriendListModel model = new FriendListModel();
                if(userId == junction.User1Id)
                {
                    model.userId = junction.User2Id;
                }
                else
                {
                    model.userId = junction.User1Id;
                }

                PublicUserProfileModel publicUserProfileModel = await _publicUserProfileService.GetUserProfileAsync(model.userId);
                model.userProfileImage = publicUserProfileModel.Photo;
                UserAccountModel userAccount = await _userAccountRepository.GetAccountById(model.userId);
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;
                model.status = publicUserProfileModel.Status;

                friendListModel.Add(model);
            }

            return friendListModel;



        }

        public async Task<IEnumerable<FriendListModel>> GetAllRequestsOutgoingAsync(int userId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _userInteractionService.GetAllUserFriendRequestsOutgoing(userId);

            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (var junction in junctionModel)
            {

                FriendListModel model = new FriendListModel();
                if (userId == junction.User1Id)
                {
                    model.userId = junction.User2Id;
                }
                else
                {
                    model.userId = junction.User1Id;
                }

                PublicUserProfileModel publicUserProfileModel = await _publicUserProfileService.GetUserProfileAsync(model.userId);
                model.userProfileImage = publicUserProfileModel.Photo;
                UserAccountModel userAccount = await _userAccountRepository.GetAccountById(model.userId);
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;
                model.status = publicUserProfileModel.Status;

                friendListModel.Add(model);
            }

            return friendListModel;



        }

        public async Task<bool> RemoveFriendAsync(int userId, int removedUserId)
        {
            return await _userInteractionService.RemoveFriendAsync(userId, removedUserId);
        }

        public async Task<bool> CancelFriendRequestAsync(int userId, int removedUserId)
        {
            return await _userInteractionService.RemoveFriendRequestAsync(userId, removedUserId);
        }

        public async Task<bool> RequestFriendAsync(int requestUserId, int requestedUserId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _userInteractionService.GetAllUserFriendRequests(requestUserId);

            foreach (FriendsListJunctionModel junction in junctionModel)
            {
                if (requestUserId == junction.User1Id && requestedUserId == junction.User2Id)
                {
                    return await ConfirmFriendAsync(requestUserId, requestedUserId);
                }
                else if (requestUserId == junction.User2Id && requestedUserId == junction.User1Id)
                {
                    return await ConfirmFriendAsync(requestUserId, requestedUserId);
                }
            }

            IEnumerable<FriendsListJunctionModel> blockModel = await _userInteractionService.GetAllUserFriendBlocks(requestUserId);

            foreach (FriendsListJunctionModel block in blockModel)
            {
                if (requestUserId == block.User1Id && requestedUserId == block.User2Id)
                {
                    return false;
                }
                else if (requestUserId == block.User2Id && requestedUserId == block.User1Id)
                {
                    return false;
                }
            }
            FriendsListJunctionModel model = new FriendsListJunctionModel();
            model.User1Id = requestUserId;
            model.User2Id = requestedUserId;
            return await _userInteractionService.CreateFriendRequestAsync(requestUserId, requestedUserId);
        }
    }
}
