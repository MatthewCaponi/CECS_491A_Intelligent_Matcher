using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.LoginTrackerRepositories
{
    public interface ILoginTrackerRepo
    {
        Task<IEnumerable<DALLoginTrackerModel>> GetAllLogins();
        Task<int> DeletebyId(int id);
        Task<int> CreateLoginTracker(DALLoginTrackerModel trackerModel);

    }
}
