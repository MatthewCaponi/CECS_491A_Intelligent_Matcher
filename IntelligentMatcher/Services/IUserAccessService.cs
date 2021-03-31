using System.Threading.Tasks;
using UserManagement.Models;

namespace IntelligentMatcher.Services
{
    public interface IUserAccessService
    {
        Task<bool> ChangeAccountStatus(int accountId, AccountStatus accountStatus);
    }
}