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
        private const int SALT_LENGTH = 10;

        private readonly IUserAccountRepository _userAccountRepository;

        public CryptographyService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }
        private async Task<bool> CreateSalt(int UserID)
        {


            // creating a StringBuilder object()
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < SALT_LENGTH; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            string salt = str_build.ToString();

            try
            {
                await _userAccountRepository.UpdateAccountSalt(UserID, salt);
                return true;
            }
            catch
            {
                return false;
            }

        }

        private async Task<string> RetreiveSaltAsync(int UserID)
        {

            string salt = await _userAccountRepository.GetSaltById(UserID);

            return salt;


        }
        private string Encrypt(string SaltedPassword)
        {
            byte[] Data = System.Text.Encoding.ASCII.GetBytes(SaltedPassword);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            return System.Text.Encoding.ASCII.GetString(Data).ToString();
        }

        public async Task<bool> NewPasswordEncryptAsync(string Password, int UserID)
        {

            bool salted = await CreateSalt(UserID);


            string salt = await _userAccountRepository.GetSaltById(UserID);
            string SaltedPassword = Password + salt;
            string encryptedPassword = Encrypt(SaltedPassword);

            await _userAccountRepository.UpdateAccountPassword(UserID, encryptedPassword);

            return true;
        }

        public async Task<string> EncryptPasswordAsync(string Password, int UserID)
        {

            string salt = await _userAccountRepository.GetSaltById(UserID);
            string SaltedPassword = Password + salt;
            string encryptedPassword = Encrypt(SaltedPassword);
            return encryptedPassword;
        }


    }
}
