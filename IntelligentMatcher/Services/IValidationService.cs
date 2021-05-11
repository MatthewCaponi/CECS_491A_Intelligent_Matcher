using System;
using System.Collections.Generic;
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
        bool ContainsNullOrEmptyParameter<T>(T model);
        bool ContainsNullOrEmptyParameter<T>(T model, List<string> exemptParameters);
        bool InvalidLength<T>(T model, int maxLength);
        bool InvalidMaxLength(string property, int maxLength);
        bool InvalidMinLength(string property, int minLength);
        bool ContainsRequiredCharacterTypes(string property, bool digit, bool upper, bool lower, bool number);
    }
}