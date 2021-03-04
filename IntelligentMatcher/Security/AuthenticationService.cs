using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Repositories;
namespace Security
{
    public class AuthenticationService : IAuthenticationService
    {

  

        public async Task<bool> AuthenticatePasswordWithUserId(string password, int userId)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            string UserHash = await userAccount.GetPasswordById(userId);

            ICryptographyService cryptographyService = new CryptographyService();

            string EnteredHash = await cryptographyService.encryptPasswordAsync(password, userId);
            if(UserHash == EnteredHash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
