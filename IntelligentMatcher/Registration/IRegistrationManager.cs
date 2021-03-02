using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;

namespace Registration
{
    public interface IRegistrationManager
    {
        Task<int> RegisterNewAccount(webUserAccountModel accountModel, webUserProfileModel usermodel);
    }
}
