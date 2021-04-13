using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Repositories;
using Models;
namespace FriendList
{
    public class FriendListManager : IFriendListManager
    {

        private readonly IFriendListRepo _friendListRepo;
        private readonly IFriendRequestListRepo _friendRequestListRepo;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IFriendBlockListRepo _friendBlockListRepo;

        public FriendListManager(IFriendListRepo friendListRepo, IFriendRequestListRepo friendRequestListRepo, IUserAccountRepository userAccountRepository, IFriendBlockListRepo friendBlockListRepo)
        {
            _friendListRepo = friendListRepo;
            _friendRequestListRepo = friendRequestListRepo;
            _userAccountRepository = userAccountRepository;
            _friendBlockListRepo = friendBlockListRepo;
        }

        public async Task<bool> BlockFriendAsync(int userId, int blockedUserId)
        {
            await _friendRequestListRepo.DeleteFriendRequestListbyUserIds(userId, blockedUserId);
            await RemoveFriendAsync(userId, blockedUserId);
            FriendsListJunctionModel model = new FriendsListJunctionModel();
            model.User1Id = userId;
            model.User2Id = blockedUserId;
            model.Date = DateTime.UtcNow;
            await _friendBlockListRepo.AddFriendBlock(model);
            return true;
        }

        public async Task<int> ConfirmFriendAsync(int requetUserId, int requestedUserId)
        {
            await _friendRequestListRepo.DeleteFriendRequestListbyUserIds(requetUserId, requestedUserId);
            FriendsListJunctionModel model = new FriendsListJunctionModel();
            model.User1Id = requetUserId;
            model.User2Id = requestedUserId;
            model.Date = DateTime.UtcNow;
            return await _friendListRepo.AddFriend(model);

        }

        public async Task<IEnumerable<FriendListModel>> GetAllFriendAsync(int userId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _friendListRepo.GetAllUserFriends(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (FriendsListJunctionModel junction in junctionModel)
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
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;

                friendListModel.Add(model);
            }

            return friendListModel;

        }
        public async Task<IEnumerable<FriendListModel>> GetAllBlocksAsync(int userId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _friendBlockListRepo.GetAllUserFriendBlocks(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (FriendsListJunctionModel junction in junctionModel)
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
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;

                friendListModel.Add(model);
            }

            return friendListModel;



        }

        public async Task<IEnumerable<FriendListModel>> GetAllBlockingUserAsync(int userId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _friendBlockListRepo.GetAllBlockingUser(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (FriendsListJunctionModel junction in junctionModel)
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
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;

                friendListModel.Add(model);
            }

            return friendListModel;



        }

        public async Task<IEnumerable<FriendListModel>> GetAllRequestsAsync(int userId)
        {
            IEnumerable< FriendsListJunctionModel> junctionModel = await _friendRequestListRepo.GetAllUserFriendRequests(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach(FriendsListJunctionModel junction in junctionModel)
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

                UserAccountModel userAccount = await _userAccountRepository.GetAccountById(model.userId);
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;

                friendListModel.Add(model);
            }

            return friendListModel;



        }

        public async Task<IEnumerable<FriendListModel>> GetAllRequestsOutgoingAsync(int userId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _friendRequestListRepo.GetAllUserFriendRequestsOutgoing(userId);
            List<FriendListModel> friendListModel = new List<FriendListModel>();

            foreach (FriendsListJunctionModel junction in junctionModel)
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
                model.username = userAccount.Username;
                model.date = DateTime.UtcNow;

                friendListModel.Add(model);
            }

            return friendListModel;



        }

        public async Task<bool> RemoveFriendAsync(int userId, int removedUserId)
        {
            await _friendListRepo.DeleteFriendListbyUserIds(userId, removedUserId);
            return true;
        }

        public async Task<bool> CancelFriendRequest(int userId, int removedUserId)
        {
            await _friendRequestListRepo.DeleteFriendRequestListbyUserIds(userId, removedUserId);
            return true;
        }

        public async Task<bool> RequestFriendAsync(int requestUserId, int requestedUserId)
        {
            IEnumerable<FriendsListJunctionModel> junctionModel = await _friendRequestListRepo.GetAllUserFriendRequests(requestUserId);

            foreach (FriendsListJunctionModel junction in junctionModel)
            {
                if (requestUserId == junction.User1Id && requestedUserId == junction.User2Id)
                {
                    await ConfirmFriendAsync(requestUserId, requestedUserId);
                    return true;
                }
                else if (requestUserId == junction.User2Id && requestedUserId == junction.User1Id)
                {
                    await ConfirmFriendAsync(requestUserId, requestedUserId);
                    return true;
                }
            }

            IEnumerable<FriendsListJunctionModel> blockModel = await _friendBlockListRepo.GetAllUserFriendBlocks(requestUserId);

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
            model.Date = DateTime.UtcNow;
            await _friendRequestListRepo.AddFriendRequest(model);
            return true;
        }
    }
}
