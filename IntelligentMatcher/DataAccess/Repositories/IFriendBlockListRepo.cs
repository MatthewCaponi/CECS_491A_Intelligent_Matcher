using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Repositories
{
    public interface IFriendBlockListRepo
    {

        Task<int> DeleteFriendBlockbyUserIds(int userId1, int userId2);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendBlocks(int userId);

        Task<int> AddFriendBlock(FriendsListJunctionModel model);

        Task<IEnumerable<FriendsListJunctionModel>> GetAllFriendBlocks();

        Task<IEnumerable<FriendsListJunctionModel>> GetAllBlockingUser(int userId);


        Task<int> DeleteFriendBlockbyId(int id);
    }
}
