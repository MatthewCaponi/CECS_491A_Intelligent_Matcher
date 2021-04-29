using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using BusinessModels;

namespace Registration
{
    public interface IRegistrationManager
    {
        Task<Result<int>> RegisterAccount(WebUserAccountModel accountModel,
            WebUserProfileModel usermodel, string password, string ipAddress);
        Task<bool> SendVerificationEmail(int accountId);

        Task DeleteIfNotActive(int userId);

    }
}
