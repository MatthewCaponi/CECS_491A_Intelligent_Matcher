﻿using DataAccess;
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
        [TestInitialize()]
        public async Task Init()
        {
            var numTestRows = 10;

            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel("TestUser" + i, "TestPass" + i, "TestEmail" + i);
                await testRepo.InsertUserAccountTestRows(userAccountModel);

                UserProfileModel model = new UserProfileModel($"FirstName {i}", $"LastName {i}", DateTime.Now, DateTime.Today.Date, 
                    UserProfileModel.AccountType.User, UserProfileModel.AccountStatus.Active, userAccountModel);

                await testRepo.InsertUserProfileTestRows(model);
            }
        }

        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

            await testRepo.DeleteUserProfileTestRows();
        }

        [DataTestMethod]
        [DataRow("John014", "532jfosho", "jfosho@gmail.com")]
        [DataRow("Lisa32", "Lisababy73", "llawrence@gmail.com")]
        [DataRow("superguy234", "ilovecats", "catlover@yahoo.com")]
        [DataRow("seiwla", "12345", "sei@gmail.com")]
        public async Task CreateUserProfile_UserProfileDoesNotExist_ReturnsCorrectId(string username, string password, string email, string firstName, string lastName, DateTime dateOfBirth, 
            DateTime accountCreationDate, UserProfileModel.AccountType accountType, UserProfileModel.AccountStatus accountStatus, UserAccountModel userAccountModel)
        {
            //Arrange
            UserAccountModel model = new UserAccountModel(username, password, email);
            UserProfileModel profileModel = new UserProfileModel(firstName, lastName, dateOfBirth, accountCreationDate, accountType, accountStatus, userAccountModel);
            IUserAccountRepository userAccount = new UserAccountRepository(new DataGateway(), new ConnectionStringData());
            IUserProfileRepository userProfile = new UserProfileRepository(new DataGateway(), new ConnectionStringData());
            await userAccount.CreateUserAccount(model);

            //Act
            await userProfile.CreateUserProfile(profileModel);
            var actualAccount = await userProfile.GetUserProfileByAccountId(userAccountModel.Id);

            //Assert
            Assert.IsTrue(actualAccount.LastName == lastName);
        }
    }
}
