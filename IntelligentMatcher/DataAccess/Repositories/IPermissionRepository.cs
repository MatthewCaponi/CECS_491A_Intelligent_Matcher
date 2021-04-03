using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IPermissionRepository
    {
        Task<int> CreatePermission(PermissionModel permissionModel);
        Task<int> DeletePermission(int id);
        Task<IEnumerable<PermissionModel>> GetAllPermissions();
    }
}
