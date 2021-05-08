using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PageVisitTrackerRepositories
{
    public interface IPageVisitTrackerRepo
    {
        Task<IEnumerable<DALPageVisitTrackerModel>> GetAllAccounts();
        Task<int> DeletebyId(int id);
        Task<int> CreatePageVisitTracker(DALPageVisitTrackerModel trackerModel);

    }
}
