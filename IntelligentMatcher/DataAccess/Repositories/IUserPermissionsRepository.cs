using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserPermissionsRepository
    {
        Task<int> CreateUserPermission(UserPermissionModel userPermissionModel);
        Task<int> DeleteUserPermission(int id);
        Task<IEnumerable<UserPermissionModel>> GetAllUserPermissions();
    }
}
