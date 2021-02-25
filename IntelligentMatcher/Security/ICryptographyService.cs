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
    public interface ICryptographyService
    {

         Task<String> CreateSalt(int UserID);

         string retreiveSalt(int UserID);
         string encrypt(string SaltedPassword);

         Boolean newPasswordEncrypt(string Password, int UserID);

         string encryptPassword(string Password, int UserID);

    }
}
