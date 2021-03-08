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


        private readonly IUserAccountRepository _userAccountRepository;

        public AuthenticationService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        public async Task<bool> AuthenticatePasswordWithEmail(string password, string email)
        {


            UserAccountModel model = await _userAccountRepository.GetAccountByEmail(email);


            string UserHash = await _userAccountRepository.GetPasswordById(model.Id);


            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());


            // Act
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepo);

            string EnteredHash = await cryptographyService.EncryptPasswordAsync(password, model.Id);
            if (UserHash != EnteredHash)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AuthenticatePasswordWithUsename(string password, string userName)
        {


            UserAccountModel model = await _userAccountRepository.GetAccountByUsername(userName);


            string UserHash = await _userAccountRepository.GetPasswordById(model.Id);

            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepo);

            string EnteredHash = await cryptographyService.EncryptPasswordAsync(password, model.Id);
            if (UserHash != EnteredHash)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AuthenticatePasswordWithUserId(string password, int userId)
        {

            string UserHash = await _userAccountRepository.GetPasswordById(userId);


            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            ICryptographyService cryptographyService = new CryptographyService(userAccountRepo);

            string EnteredHash = await cryptographyService.EncryptPasswordAsync(password, userId);
            if(UserHash != EnteredHash)
            {
                return false;
            }
            return true;
        }
    }
}
