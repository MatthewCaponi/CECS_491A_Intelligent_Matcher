using System;
using System.Collections.Generic;
using System.Text;
using Models;
using DataAccess;
using DataAccess.Repositories;
using System.Threading.Tasks;
using Models;
namespace Services
{
    public class UserInteractionService : IUserInteractionService
    {
        IFriendBlockListRepo _friendBlockListRepo;
        IFriendRequestListRepo _friendRequestListRepo;
        IFriendListRepo _friendListRepo;
        IUserReportsRepo _userReportsRepo;
        public UserInteractionService(IFriendBlockListRepo friendBlockListRepo, IFriendListRepo friendListRepo,  IFriendRequestListRepo friendRequestListRepo, IUserReportsRepo userReportsRepo)
        {
            _friendBlockListRepo = friendBlockListRepo;
            _friendListRepo = friendListRepo;
            _friendRequestListRepo = friendRequestListRepo;
            _userReportsRepo = userReportsRepo;
        }

        public async Task<bool> CreateReportAsync(UserReportsModel model)
        {
            model.Date = DateTime.UtcNow;
            await _userReportsRepo.CreateReport(model);
            return true;
        }

        public async Task<bool> CreateFriendshipAsync(int userId1, int userId2)
        {
            FriendsListJunctionModel model = new FriendsListJunctionModel();
            model.User1Id = userId1;
            model.User2Id = userId2;
            model.Date = DateTime.UtcNow;
            await _friendListRepo.AddFriend(model);
            return true;
        }

        public async Task<bool> CreateFriendRequestAsync(int userId1, int userId2)
        {
            FriendsListJunctionModel model = new FriendsListJunctionModel();
            model.User1Id = userId1;
            model.User2Id = userId2;
            model.Date = DateTime.UtcNow;
            await _friendRequestListRepo.AddFriendRequest(model);
            return true;
        }

        public async Task<bool> CreateBlockAsync(int userId, int blockedUserId)
        {
            FriendsListJunctionModel model = new FriendsListJunctionModel();
            model.User1Id = userId;
            model.User2Id = blockedUserId;
            model.Date = DateTime.UtcNow;
            await _friendBlockListRepo.AddFriendBlock(model);
            return true;
        }

        public async Task<bool> RemoveFriendAsync(int userId, int removedUserId)
        {
            await _friendListRepo.DeleteFriendListbyUserIds(userId, removedUserId);
            return true;
        }

        public async Task<bool> RemoveFriendRequestAsync(int userId, int removedUserId)
        {
            await _friendRequestListRepo.DeleteFriendRequestListbyUserIds(userId, removedUserId);
            return true;
        }

        public async Task<bool> RemoveFriendBlockAsync(int userId, int blockedUserId)
        {
            await _friendRequestListRepo.DeleteFriendRequestListbyUserIds(userId, blockedUserId);
            return true;
        }


        public async Task<bool> RemoveReportAsync(int reportId)
        {
            await _userReportsRepo.DeleteReportById(reportId);
            return true;
        }
    }
}
