using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ContextRepository : IContextRepository
    {
        public Task<int> CreateContextItem(ContextModel contextModel)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteeContextItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ContextModel>> GetAllContextItems()
        {
            throw new NotImplementedException();
        }
    }
}
