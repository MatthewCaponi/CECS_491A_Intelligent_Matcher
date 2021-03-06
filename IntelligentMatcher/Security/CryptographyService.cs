using DataAccess;
using DataAccess.Repositories;
using System;
using static Models.UserProfileModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Security
{
    public class CryptographyService : ICryptographyService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public CryptographyService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        private async Task<bool> CreateSalt(int UserID)
        {

            int length = 10;

            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            string salt = str_build.ToString();


            await _userAccountRepository.UpdateAccountSalt(UserID, salt);

            return (true);

        }

        private async Task<string> retreiveSaltAsync(int UserID)
        {

            string salt = await _userAccountRepository.GetSaltById(UserID);

            return salt;


        }
        private string encrypt(string SaltedPassword)
        {
            byte[] Data = System.Text.Encoding.ASCII.GetBytes(SaltedPassword);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            return (System.Text.Encoding.ASCII.GetString(Data).ToString());
        }

        public async Task<bool> newPasswordEncryptAsync(string Password, int UserID)
        {

            bool salted = await CreateSalt(UserID);


            string salt = await _userAccountRepository.GetSaltById(UserID);
            string SaltedPassword = Password + salt;
            string encryptedPassword = encrypt(SaltedPassword);

            await _userAccountRepository.UpdateAccountPassword(UserID, encryptedPassword);

            return (true);
        }

        public async Task<string> encryptPasswordAsync(string Password, int UserID)
        {

            string salt = await _userAccountRepository.GetSaltById(UserID);
            string SaltedPassword = Password + salt;
            string encryptedPassword = encrypt(SaltedPassword);
            return (encryptedPassword);
        }

        Task<string> ICryptographyService.CreateSalt(int UserID)
        {
            throw new NotImplementedException();
        }

        string ICryptographyService.retreiveSalt(int UserID)
        {
            throw new NotImplementedException();
        }

        string ICryptographyService.encrypt(string SaltedPassword)
        {
             throw new NotImplementedException();
        }
    }
}
