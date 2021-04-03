using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PermissionContextRepository : IPermissionContextRepository
    {
        public Task<int> CreatePermissionContext(PermissionContextModel permissionContextModel)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePermissionContext(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PermissionContextModel>> GetAllPermissionContexts()
        {
            throw new NotImplementedException();
        }
    }
}
