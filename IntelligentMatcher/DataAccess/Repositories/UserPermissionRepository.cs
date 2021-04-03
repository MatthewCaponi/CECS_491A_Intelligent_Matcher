using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserPermissionRepository : IUserPermissionsRepository
    {
        public Task<int> CreateUserPermission(UserPermissionModel userPermissionModel)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteUserPermission(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserPermissionModel>> GetAllUserPermissions()
        {
            throw new NotImplementedException();
        }
    }
}
