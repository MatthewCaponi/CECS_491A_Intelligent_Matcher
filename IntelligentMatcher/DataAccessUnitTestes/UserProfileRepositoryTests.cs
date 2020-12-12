using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessUnitTestes
{
    [TestClass]
    public class UserProfileRepositoryTests
    {
        //[TestInitialize()]
        //public async Task Init()
        //{
        //    var numTestRows = 10;

        //    IDataGateway dataGateway = new DataGateway();
        //    IConnectionStringData connectionString = new ConnectionStringData();
        //    ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

        //    for (int i = 1; i <= numTestRows; ++i)
        //    {
        //        UserAccountModel userAccountModel = new UserAccountModel("TestUser" + i, "TestPass" + i, "TestEmail" + i);
        //        await testRepo.InsertUserAccountTestRows(userAccountModel); 

        //        UserProfileModel model = new UserProfileModel($"FirstName {i}", $"LastName {i}", DateTime.Now, DateTime.Today.Date,
        //             UserProfileModel.AccountType.User.ToString(), UserProfileModel.AccountStatus.Active.ToString(), userAccountModel);
        //        await testRepo.InsertUserProfileTestRows(model);

        //    }





        //}

        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

            await testRepo.DeleteUserAccountTestRows();
            await testRepo.DeleteUserProfileTestRows();
        }

        [DataTestMethod]
        [DataRow("John014", "532jfosho", "jfosho@gmail.com", "John", "Fosho", UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Banned)]
        [DataRow("Lisa32", "Lisababy73", "llawrence@gmail.com", "Lisa", "Baby", UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Banned)]
        [DataRow("superguy234", "ilovecats", "catlover@yahoo.com", "Super", "Guy", UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Banned)]
        [DataRow("seiwla", "12345", "sei@gmail.com", "Sei", "wa", UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Banned)]
        public async Task CreateUserProfile_UserProfileDoesNotExist_ReturnsCorrectId(string username, string password, string email, string firstName, string lastName, 
        UserProfileModel.AccountType accountType, UserProfileModel.AccountStatus accountStatus)
        {
            //Arrange
            UserAccountModel userAccountModel = new UserAccountModel(username, password, email);
            UserProfileModel profileModel = new UserProfileModel(firstName, lastName, DateTime.Now, DateTime.Today, accountType.ToString(), accountStatus.ToString(), userAccountModel);
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());
            await userAccount.CreateUserAccount(userAccountModel);

            //Act
            await userProfile.CreateUserProfile(profileModel);
            var actualAccount = await userProfile.GetUserProfileByAccountId(userAccountModel.Id);

            //Assert
            Assert.IsTrue(actualAccount.LastName == lastName);
        }
    }
}
