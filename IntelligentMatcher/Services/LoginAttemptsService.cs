using BusinessModels;
using DataAccess.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoginAttemptsService : ILoginAttemptsService
    {
        private readonly ILoginAttemptsRepository _loginAttemptsRepository;

        public LoginAttemptsService(ILoginAttemptsRepository loginAttemptsRepository)
        {
            _loginAttemptsRepository = loginAttemptsRepository;
        }

        public async Task<bool> AddIpAddress(string ipAddress, int loginCounter)
        {
            try
            {
                LoginAttemptsModel loginAttemptsModel = new LoginAttemptsModel();

                loginAttemptsModel.IpAddress = ipAddress;
                loginAttemptsModel.LoginCounter = loginCounter;
                loginAttemptsModel.SuspensionEndTime = DateTimeOffset.UtcNow;

                var changesMade = await _loginAttemptsRepository.CreateLoginAttempts(loginAttemptsModel);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<BusinessLoginAttemptsModel> GetLoginAttemptsByIpAddress(string ipAddress)
        {
            try
            {
                var loginAttemptsModel = await _loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);

                if (loginAttemptsModel == null)
                {
                    return null;
                }
                else
                {
                    var businessLoginAttemptsModel = ModelConverterService.ConvertTo(loginAttemptsModel,
                        new BusinessLoginAttemptsModel());

                    return businessLoginAttemptsModel;
                }
            }
            catch
            {
                return null;
            }
            
        }

        public async Task<bool> IncrementLoginCounterByIpAddress(string ipAddress)
        {
            try
            {
                var changesMade = await _loginAttemptsRepository.IncrementLoginCounterByIpAddress(ipAddress);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ResetLoginCounterByIpAddress(string ipAddress)
        {
            try
            {
                var changesMade = await _loginAttemptsRepository.ResetLoginCounterByIpAddress(ipAddress);

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SetSuspensionEndTimeByIpAddress(string ipAddress, int suspensionHours)
        {
            try
            {
                var changesMade = await _loginAttemptsRepository.UpdateSuspensionEndTimeByIpAddress
                (ipAddress, DateTimeOffset.UtcNow.AddHours(suspensionHours));

                if (changesMade == 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
