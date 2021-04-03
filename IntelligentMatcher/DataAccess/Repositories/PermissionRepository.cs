using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        public Task<int> CreatePermission(PermissionModel permissionModel)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePermission(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionModel>> GetAllPermissions()
        {
            throw new NotImplementedException();
        }
    }
}
