using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Repositories;
namespace Security
{
    public interface IPasswordValidationService
    {
        Task<bool> AuthenticatePasswordWithUserId(string password, int userId);

        Task<bool> AuthenticatePasswordWithEmail(string password, string email);

        Task<bool> AuthenticatePasswordWithUsename(string password, string userName);
    }
}
