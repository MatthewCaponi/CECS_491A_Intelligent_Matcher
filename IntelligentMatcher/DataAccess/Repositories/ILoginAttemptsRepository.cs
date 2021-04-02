using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ILoginAttemptsRepository
    {
        Task<int> CreateLoginAttempts(LoginAttemptsModel model);
        Task<int> DeleteLoginAttemptsById(int id);
        Task<IEnumerable<LoginAttemptsModel>> GetAllLoginAttempts();
        Task<LoginAttemptsModel> GetLoginAttemptsById(int id);
        Task<LoginAttemptsModel> GetLoginAttemptsByIpAddress(string ipAddress);
        Task<int> IncrementLoginCounterById(int id);
        Task<int> IncrementLoginCounterByIpAddress(string ipAddress);
        Task<int> ResetLoginCounterById(int id);
        Task<int> ResetLoginCounterByIpAddress(string ipAddress);
        Task<int> UpdateSuspensionEndTimeById(int id, DateTimeOffset suspensionEndTime);
        Task<int> UpdateSuspensionEndTimeByIpAddress(string ipAddress, DateTimeOffset suspensionEndTime);
    }
}
