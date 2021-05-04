using BusinessModels;
using DataAccess.Repositories;
using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserAccountCodeService : IUserAccountCodeService
    {
        private readonly IUserAccountCodeRepository _userAccountCodeRepository;

        public UserAccountCodeService(IUserAccountCodeRepository userAccountCodeRepository)
        {
            _userAccountCodeRepository = userAccountCodeRepository;
        }

        public async Task<bool> AddCode(string code, DateTimeOffset expirationTime, int accountId)
        {
            try
            {
                UserAccountCodeModel userAccountCodeModel = new UserAccountCodeModel();

                userAccountCodeModel.Code = code;
                userAccountCodeModel.ExpirationTime = expirationTime;
                userAccountCodeModel.UserAccountId = accountId;

                var changesMade = await _userAccountCodeRepository.CreateUserAccountCode(userAccountCodeModel);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> UpdateCodeByAccountId(string code, DateTimeOffset expirationTime, int accountId)
        {
            try
            {
                var changesMade = await _userAccountCodeRepository.UpdateUserAccountCodeByAccountId(code, expirationTime,
                    accountId);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<bool> DeleteCodeByAccountId(int accountId)
        {
            try
            {
                var changesMade = await _userAccountCodeRepository.DeleteUserAccountCodeByAccountId(accountId);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<BusinessUserAccountCodeModel> GetUserAccountCodeByAccountId(int accountId)
        {
            try
            {
                var userAccountCodeModel = await _userAccountCodeRepository.GetUserAccountCodeByAccountId(accountId);

                if(userAccountCodeModel == null)
                {
                    return null;
                }
                else
                {
                    var businessUserAccountCodeModel = ModelConverterService.ConvertTo(userAccountCodeModel,
                        new BusinessUserAccountCodeModel());

                    return businessUserAccountCodeModel;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message, e.InnerException);
            }
        }
    }
}
