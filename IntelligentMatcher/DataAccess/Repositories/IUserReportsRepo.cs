using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace DataAccess.Repositories
{
    public interface IUserReportsRepo
    {

        Task<int> DeleteReportById(int id);

        Task<IEnumerable<UserReportsModel>> GetAllReports(int userId);

        Task<int> CreateReport(UserReportsModel model);
    }
}
