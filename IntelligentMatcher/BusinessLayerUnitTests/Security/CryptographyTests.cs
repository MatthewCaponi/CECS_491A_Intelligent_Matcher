using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Security;
using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;
using Services;
namespace BusinessLayerUnitTests.Security
{
    [TestClass]
    public class CyrptographyTests
    {
        
     
    

        [DataTestMethod]
        [DataRow("Password", 1)]
        public async Task newPasswordTest(string password, int UserID)
        {


            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());


            // Act
            ICryptographyService CryptographyService = new CryptographyService();

            await CryptographyService.newPasswordEncryptAsync(password, UserID);


            var userAccount = await userAccountRepo.GetAccountById(UserID);
            var encryptedPassword = userAccount.Password;

            string salt = await userAccountRepo.GetSaltById(UserID);

            string SaltedPassword = password + salt;

            byte[] Data = System.Text.Encoding.ASCII.GetBytes(SaltedPassword);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            string testPassword = System.Text.Encoding.ASCII.GetString(Data).ToString();


            //Assert
            Assert.IsTrue(testPassword == encryptedPassword);



        }




        [DataTestMethod]
        [DataRow("Password", 1)]
        public async Task oldPasswordTest(string password, int UserID)
        {


            UserAccountRepository userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());


            // Act
            ICryptographyService CryptographyService = new CryptographyService();

            string encryptedPassedPassword = await CryptographyService.encryptPasswordAsync(password, UserID);


            var userAccount = await userAccountRepo.GetAccountById(UserID);
            var encryptedPassword = userAccount.Password;

            string salt = await userAccountRepo.GetSaltById(UserID);

            string SaltedPassword = password + salt;

            byte[] Data = System.Text.Encoding.ASCII.GetBytes(SaltedPassword);
            Data = new System.Security.Cryptography.SHA256Managed().ComputeHash(Data);
            string testPassword = System.Text.Encoding.ASCII.GetString(Data).ToString();


            //Assert
            Assert.IsTrue(testPassword == encryptedPassedPassword);



        }





    }
}
