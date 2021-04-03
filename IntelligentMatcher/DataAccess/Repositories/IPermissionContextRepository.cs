using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IPermissionContextRepository
    {
        Task<int> CreatePermissionContext(PermissionContextModel permissionContextModel);
        Task<int> DeletePermissionContext(int id);
        Task<IEnumerable<PermissionContextModel>> GetAllPermissionContexts();
    }
}
