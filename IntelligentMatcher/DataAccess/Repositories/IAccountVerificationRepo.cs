using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IAccountVerificationRepo
    {

        Task<string> GetStatusTokenByUserId(int userId);

        Task<int> UpdateAccountStatusToken(int userId, string token);

        Task<int> CreateAccountVerification(int userId, string token);

        Task<int> DeleteAccountVerificationById(int id);
        

        Task<IEnumerable<VerficationTokenModel>> GetAllAccountVerifications();
    }
}
