using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Repositories
{
    public interface IFriendListRepo
    {

        Task<int>  DeleteFriendListbyUserIds(int userId1, int userId2);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriends(int userId);

        Task<int> AddFriend(FriendsListJunctionModel model);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllFriends();

        Task<int> DeleteFriendListbyId(int id);
    }
}
