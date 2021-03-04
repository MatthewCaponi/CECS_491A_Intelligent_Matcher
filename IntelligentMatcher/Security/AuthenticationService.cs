using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Repositories;
using Models;
namespace Security
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> AuthenticatePasswordWithEmail(string password, string email)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);

            UserAccountModel model = await userAccount.GetAccountByEmail(email);


            string UserHash = await userAccount.GetPasswordById(model.Id);

            ICryptographyService cryptographyService = new CryptographyService();

            string EnteredHash = await cryptographyService.encryptPasswordAsync(password, model.Id);
            if (UserHash == EnteredHash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AuthenticatePasswordWithUsename(string password, string userName)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);

            UserAccountModel model = await userAccount.GetAccountByUsername(userName);


            string UserHash = await userAccount.GetPasswordById(model.Id);

            ICryptographyService cryptographyService = new CryptographyService();

            string EnteredHash = await cryptographyService.encryptPasswordAsync(password, model.Id);
            if (UserHash == EnteredHash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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
