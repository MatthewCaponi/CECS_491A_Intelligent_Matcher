using System;

namespace Security
{
    class ICryptographyService
    {

        private string createSalt(int UserID)
        {
            return "Salt";
        }

        private string retreiveSalt(int UserID)
        {
            return "Salt";
        }
        private string encrypt(string SaltedPassword)
        {
            byte[] Data = System.Text.Encoding.ASCII.GetBytes(SaltedPassword);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            return (System.Text.Encoding.ASCII.GetString(Data).ToString());
        }

        public Boolean newPasswordEncrypt(string Password, int UserID)
        {
            string SaltedPassword = Password + createSalt(UserID);
            string encryptedPassword = encrypt(SaltedPassword);
            return (true);
        }

        public string encryptPassword(string Password, int UserID)
        {
            string SaltedPassword = Password + retreiveSalt(UserID);
            string encryptedPassword = encrypt(SaltedPassword);
            return (encryptedPassword);
        }

    }
}
