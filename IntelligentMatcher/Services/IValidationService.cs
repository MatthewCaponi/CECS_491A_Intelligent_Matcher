using System;
using System.Threading.Tasks;
using UserManagement.Models;

namespace Services
{
    public interface IValidationService
    {
        Task<bool> EmailExists(string emailAddress);
        bool IsNull(object obj);
        Task<bool> UserExists(int id);
        Task<bool> UsernameExists(string username);
        Task<bool> UserIsActive(int id);      
    }
}