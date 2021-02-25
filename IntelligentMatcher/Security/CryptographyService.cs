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
    class CryptographyService : ICryptographyService
    {
        private async Task<String> CreateSalt(int UserID)
        {

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[1024];

            rng.GetBytes(buffer);

            string salt = BitConverter.ToString(buffer); IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);

            try
            {
                var returned = await userAccount.UpdateAccountSalt(UserID, salt);
            }
            catch (Exception e)
            {
            }

            return salt;

        }

        private string retreiveSalt(int UserID)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccount = new UserAccountRepository(dataGateway, connectionString);
            string salt = userAccount.GetSaltById(UserID).ToString();

            return salt;


        }
        private string encrypt(string SaltedPassword)
        {
            byte[] Data = System.Text.Encoding.ASCII.GetBytes(SaltedPassword);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            return (System.Text.Encoding.ASCII.GetString(Data).ToString());
        }

        public Boolean newPasswordEncrypt(string Password, int UserID)
        {
            string SaltedPassword = Password + CreateSalt(UserID);
            string encryptedPassword = encrypt(SaltedPassword);
            return (true);
        }

        public string encryptPassword(string Password, int UserID)
        {
            string SaltedPassword = Password + retreiveSalt(UserID);
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
