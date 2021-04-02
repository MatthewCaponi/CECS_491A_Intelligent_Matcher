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

        public async Task<bool> AddIpAddress(string ipAddress)
        {
            LoginAttemptsModel loginAttemptsModel = new LoginAttemptsModel();

            loginAttemptsModel.IpAddress = ipAddress;
            loginAttemptsModel.LoginCounter = 0;
            loginAttemptsModel.SuspensionEndTime = DateTimeOffset.UtcNow;

            await _loginAttemptsRepository.CreateLoginAttempts(loginAttemptsModel);

            return true;
        }

        public async Task<bool> CheckLoginCounterByIpAddress(string ipAddress)
        {
            var loginAttemptModel = await _loginAttemptsRepository.GetLoginAttemptsByIpAddress(ipAddress);

            if(loginAttemptModel == null)
            {
                await AddIpAddress(ipAddress);
            }

            if(loginAttemptModel.LoginCounter >= 5)
            {
                if(DateTimeOffset.UtcNow < loginAttemptModel.SuspensionEndTime)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> IncrementLoginCounterByIpAddress(string ipAddress)
        {
            await _loginAttemptsRepository.IncrementLoginCounterByIpAddress(ipAddress);

            return true;
        }

        public async Task<bool> ResetLoginCounterByIpAddress(string ipAddress)
        {
            await _loginAttemptsRepository.ResetLoginCounterByIpAddress(ipAddress);

            return true;
        }

        public async Task<bool> SetSuspensionEndTimeByIpAddress(string ipAddress)
        {
            await _loginAttemptsRepository.UpdateSuspensionEndTimeByIpAddress(ipAddress, DateTimeOffset.UtcNow.AddHours(1));

            return true;
        }
    }
}
