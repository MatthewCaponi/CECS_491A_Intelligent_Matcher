using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace FriendList
{
    public interface IFriendListManager
    {

        Task<string> GetFriendStatusUserIdAsync(int userId1, int userId2);
        Task<bool> RequestFriendAsync(int requestUserId, int requestedUserId);

        Task<int> ConfirmFriendAsync(int requetUserId, int requestedUserId);

        Task<bool> RemoveFriendAsync(int userId, int removedUserId);

        Task<bool> BlockFriendAsync(int userId, int blockedUserId);

        Task<bool> CancelFriendRequestAsync(int userId, int blockedUserId);

        Task<IEnumerable<FriendListModel>> GetAllBlocksAsync(int userId);

        Task<IEnumerable<FriendListModel>> GetAllBlockingUserAsync(int userId);

        Task<IEnumerable<FriendListModel>> GetAllFriendAsync(int userId);

        Task<IEnumerable<FriendListModel>> GetAllRequestsAsync(int userId);

        Task<IEnumerable<FriendListModel>> GetAllRequestsOutgoingAsync(int userId);
    }
}
