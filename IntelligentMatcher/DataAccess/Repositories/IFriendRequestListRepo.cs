using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Repositories
{
    public interface IFriendRequestListRepo
    {

        Task<int> DeleteFriendRequestListbyUserIds(int userId1, int userId2);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendRequests(int userId);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendRequestsOutgoing(int userId);


        Task<int> AddFriendRequest(FriendsListJunctionModel model);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllFriendRequests();

        Task<int> DeleteFriendRequestListbyId(int id);
    }
}
