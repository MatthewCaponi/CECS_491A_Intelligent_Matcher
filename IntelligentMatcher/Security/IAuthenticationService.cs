using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Repositories;
namespace Security
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticatePasswordWithUserId(string password, int userId);
    }
}
