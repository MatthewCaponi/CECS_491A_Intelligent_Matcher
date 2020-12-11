using Models;
using System.Threading.Tasks;

namespace DataAccessUnitTestes
{
    public interface ITestRepo
    {
        Task<int> DeleteUserAccountTestRows();
        Task<int> DeleteUserProfileTestRows();
        Task<int> InsertUserAccountTestRows(UserAccountModel model);
        Task<int> InsertUserProfileTestRows(UserProfileModel model);
    }
}