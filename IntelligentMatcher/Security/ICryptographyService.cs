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



         Task<bool> newPasswordEncryptAsync(string Password, int UserID);

         Task<string> EncryptPasswordAsync(string Password, int UserID);

    }
}
