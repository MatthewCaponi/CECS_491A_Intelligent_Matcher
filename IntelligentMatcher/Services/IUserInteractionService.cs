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

        Task<bool> CreateFriendRequestAsync(int userId1, int userId2);

        Task<bool> CreateBlockAsync(int userId, int blockedUserId);

        Task<bool> RemoveFriendAsync(int userId, int removedUserId);

        Task<bool> RemoveFriendRequestAsync(int userId, int removedUserId);

        Task<bool> RemoveFriendBlockAsync(int userId, int blockedUserId);

        Task<bool> RemoveReportAsync(int reportId);
    }
}
