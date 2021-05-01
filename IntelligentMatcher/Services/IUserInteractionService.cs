using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserInteractionService
    {
        Task<bool> CreateReportAsync(UserReportsModel model);

        Task<bool> CreateFriendshipAsync(int userId1, int userId2);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllBlockingUsers(int userId);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendRequests(int userId);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendRequestsOutgoing(int userId);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendBlocks(int userId);

        Task<bool> CreateFriendRequestAsync(int userId1, int userId2);

        Task<bool> CreateBlockAsync(int userId, int blockedUserId);

        Task<bool> RemoveFriendAsync(int userId, int removedUserId);

        Task<bool> RemoveFriendRequestAsync(int userId, int removedUserId);

        Task<bool> RemoveFriendBlockAsync(int userId, int blockedUserId);

        Task<bool> RemoveReportAsync(int reportId);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriends(int userId);
    }
}
