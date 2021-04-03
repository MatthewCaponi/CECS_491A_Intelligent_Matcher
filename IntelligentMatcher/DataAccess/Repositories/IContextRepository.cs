using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IContextRepository
    {
        Task<int> CreateContextItem(ContextModel contextModel);
        Task<int> DeleteeContextItem(int id);
        Task<IEnumerable<ContextModel>> GetAllContextItems();
    }
}
