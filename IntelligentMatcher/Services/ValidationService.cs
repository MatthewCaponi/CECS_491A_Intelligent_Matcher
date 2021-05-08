using IntelligentMatcher.Services;
using System;
using UserManagement.Models;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Services;
using Exceptions;
using System.Collections.Generic;
using System.Reflection;

namespace Services
{
    public class ValidationService : IValidationService
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IUserProfileService _userProfileService;

        public ValidationService(IUserAccountService userAccountService, IUserProfileService userProfileService)
        {
            _userAccountService = userAccountService;
            _userProfileService = userProfileService;
        }

        //come back to this
        public bool IsNull(object obj)
        {
            if (!(obj is null))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UserExists(int id)
        {
            try
            {
                var userAccounts = await (_userAccountService.GetAllUserAccounts());
                if (userAccounts.Any(x => x.Id == id))
                {
                    return true;
                }

                return false;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> UsernameExists(string username)
        {
            try
            {
                var userAccounts = await _userAccountService.GetAllUserAccounts();
                if (userAccounts.Any(x => x.Username == username))
                {
                    return true;
                }

                return false;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> EmailExists(string emailAddress)
        {
            try
            {
                var userAccounts = await _userAccountService.GetAllUserAccounts();
                if (userAccounts.Any(x => x.EmailAddress == emailAddress))
                {
                    return true;
                }

                return false;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> UserIsActive(int id)
        {
            try
            {
                var userAccount = await _userAccountService.GetUserAccount(id);
                if (userAccount.AccountStatus == AccountStatus.Active.ToString())
                {
                    return true;
                }

                return false;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public bool ContainsNullOrEmptyParameter<T>(T model)
        {
            if (ContainsNullOrEmptyParameter(model, null))
            {
                return true;
            }

            return false;
        }

        public bool ContainsNullOrEmptyParameter<T>(T model, List<string> exemptParameters)
        {
            int i = 0;
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                if (Nullable.GetUnderlyingType(property.GetType()) == null)
                {
                    if (exemptParameters != null)
                    {
                        if (property.Name.ToString() == exemptParameters[i])
                        {
                            continue;
                        }
                    }

                    if (property.GetValue(model, null) == null)
                    {
                        return false;
                    }
                }

                if (property.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty((string)property.GetValue(model, null)))
                    {
                        return false;
                    }
                }

                ++i;
            }

            return true;
        }

        public bool InvalidLength<T>(T model, int maxLength)
        {
            foreach (PropertyInfo property in model.GetType().GetProperties())
            {
                if (property.GetType() == typeof(string))
                {
                    if (property.GetValue(model, null).ToString().Length >= maxLength)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool InvalidMaxLength(string property, int maxLength)
        {
            if (property.Length >= maxLength)
            {
                return false;
            }

            return true;
        }

        public bool InvalidMinLength(string property, int minLength)
        {
            if (property.Length < minLength)
            {
                return false;
            }

            return true;
        }

        public bool ContainsRequiredCharacterTypes(string property, bool digit, bool upper, bool lower, bool number)
        {
            if (digit)
            {
                if (!property.Any(char.IsDigit))
                {
                    return false;
                }
            }
            if (upper)
            {
                if (!property.Any(char.IsUpper))
                {
                    return false;
                }
            }
            if (lower)
            {
                if (!property.Any(char.IsLower))
                {
                    return false;
                }
            }
            if (number)
            {
                if (!property.Any(char.IsNumber))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
