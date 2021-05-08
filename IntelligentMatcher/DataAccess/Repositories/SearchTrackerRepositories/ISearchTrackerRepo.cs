using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.SearchTrackerRepositories
{
    public interface ISearchTrackerRepo
    {
        Task<IEnumerable<DALSearchTrackerModel>> GetAllSearches();
        Task<int> DeletebyId(int id);
        Task<int> CreatePageVisitTracker(DALSearchTrackerModel trackerModel);




    }
}
