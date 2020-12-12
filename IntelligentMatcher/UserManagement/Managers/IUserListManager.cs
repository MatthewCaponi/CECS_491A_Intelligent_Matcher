using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement
{
    public interface IUserListManager
    {
        Task<List<UserListTransferModel>> PopulateListOfUsers();
    }
}