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
        private static IValidationService _validationService;
        IFriendBlockListRepo _friendBlockListRepo;
        IFriendRequestListRepo _friendRequestListRepo;
        IFriendListRepo _friendListRepo;
        IUserReportsRepo _userReportsRepo;
        public UserInteractionService(IFriendBlockListRepo friendBlockListRepo, IFriendListRepo friendListRepo,  IFriendRequestListRepo friendRequestListRepo, IUserReportsRepo userReportsRepo, IValidationService validationService)
        {
            _friendBlockListRepo = friendBlockListRepo;
            _friendListRepo = friendListRepo;
            _friendRequestListRepo = friendRequestListRepo;
            _userReportsRepo = userReportsRepo;
            _validationService = validationService;
        }

        public async Task<bool> CreateReportAsync(UserReportsModel model)
        {
            if (_validationService.IsNull(model))
            {
                try
                {
                    model.Date = DateTime.UtcNow;
                    await _userReportsRepo.CreateReport(model);
                    return true;
                }
                catch
                {
                    return false;
                }

            }

                return false;
            
        }


        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriends(int userId)
        {
            if (_validationService.IsNull(userId))
            {
                try
                {
                    return await _friendListRepo.GetAllUserFriends(userId);
                }
                catch
                {
                    return null;
                }
            }

            return null;
            
        
        }

 

        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllBlockingUsers(int userId)
        {
            if (_validationService.IsNull(userId))
            {
                try
                {
                    return await _friendBlockListRepo.GetAllBlockingUser(userId);
                }
                catch
                {
                    return null;
                }
            }

            return null;
            
        }

        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendRequestsOutgoing(int userId)
        {
            if (_validationService.IsNull(userId))
            {
                try
                {
                    return await _friendRequestListRepo.GetAllUserFriendRequestsOutgoing(userId);
                }
                catch
                {
                    return null;
                }
            }

            return null;
        
        }

        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendBlocks(int userId)
        {
            if (_validationService.IsNull(userId))
            {
                try
                {
                    return await _friendBlockListRepo.GetAllUserFriendBlocks(userId);
                }
                catch
                {
                    return null;
                }
            }
       
            
            return null;
            
        }

        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendRequests(int userId)
        {
            if (_validationService.IsNull(userId))
            {
                try
                {
                    return await _friendRequestListRepo.GetAllUserFriendRequests(userId);
                }
                catch
                {
                    return null;
                }
            }
 
            return null;
           
        }

        public async Task<bool> CreateFriendshipAsync(int userId1, int userId2)
        {
            if (_validationService.IsNull(userId1) && _validationService.IsNull(userId2))
            {

                try
                {
                    FriendsListJunctionModel model = new FriendsListJunctionModel();
                    model.User1Id = userId1;
                    model.User2Id = userId2;
                    model.Date = DateTime.UtcNow;
                    await _friendListRepo.AddFriend(model); 
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;


        }

        public async Task<bool> CreateFriendRequestAsync(int userId1, int userId2)
        {
            if (_validationService.IsNull(userId1) && _validationService.IsNull(userId2))
            {

                try
                {
                    FriendsListJunctionModel model = new FriendsListJunctionModel();
                    model.User1Id = userId1;
                    model.User2Id = userId2;
                    model.Date = DateTime.UtcNow;
                    await _friendRequestListRepo.AddFriendRequest(model); 
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> CreateBlockAsync(int userId, int blockedUserId)
        {
            if (_validationService.IsNull(userId) && _validationService.IsNull(blockedUserId))
            {

                try
                {
                    FriendsListJunctionModel model = new FriendsListJunctionModel();
                    model.User1Id = userId;
                    model.User2Id = blockedUserId;
                    model.Date = DateTime.UtcNow;
                    await _friendBlockListRepo.AddFriendBlock(model); 
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> RemoveFriendAsync(int userId, int removedUserId)
        {
            if (_validationService.IsNull(userId) && _validationService.IsNull(removedUserId))
            {
                try
                {
                    await _friendListRepo.DeleteFriendListbyUserIds(userId, removedUserId);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> RemoveFriendRequestAsync(int userId, int removedUserId)
        {
            if (_validationService.IsNull(userId) && _validationService.IsNull(removedUserId))
            {
                try
                {
                    await _friendRequestListRepo.DeleteFriendRequestListbyUserIds(userId, removedUserId);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<bool> RemoveFriendBlockAsync(int userId, int blockedUserId)
        {
            if (_validationService.IsNull(userId) && _validationService.IsNull(blockedUserId))
            {
                try
                {
                    await _friendRequestListRepo.DeleteFriendRequestListbyUserIds(userId, blockedUserId);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }


        public async Task<bool> RemoveReportAsync(int reportId)
        {
            if (_validationService.IsNull(reportId))
            {
                try
                {
                    await _userReportsRepo.DeleteReportById(reportId);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            return false;


        }
    }
}
