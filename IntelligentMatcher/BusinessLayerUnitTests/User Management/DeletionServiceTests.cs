using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Services;

namespace BusinessLayerUnitTests.User_Management
{
    [TestClass]
    public class DeletionServiceTests
    {
        [TestClass]
        public class UserAccessServiceTests
        {
            [TestInitialize]
            public async Task Init()
            {
                var numTestRows = 10;

                IDataGateway dataGateway = new DataGateway();
                IConnectionStringData connectionString = new ConnectionStringData();
                ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

                for (int i = 1; i <= numTestRows; ++i)
                {
                    UserAccountModel userAccountModel = new UserAccountModel("TestUser" + i, "TestPass" + i, "TestEmail" + i);
                    userAccountModel.Id = await testRepo.InsertUserAccountTestRows(userAccountModel);

                    UserProfileModel model = new UserProfileModel($"FirstName{i}", $"LastName{i}", DateTime.Now, DateTime.Today.Date,
                         UserProfileModel.AccountType.User.ToString(), UserProfileModel.AccountStatus.Active.ToString(), userAccountModel.Id);
                    await testRepo.InsertUserProfileTestRows(model);
                }
            }

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
            [DataRow(3)]
            public async Task DeleteAccount_AccountExists_AccountDeleted(int userId)
            {
                // Arrange
                var userAccountRepo = new UserAccountRepository(new DataGateway(), new ConnectionStringData());

                // Act
                try
                {
                    await DeletionService.DeleteAccount(userId);
                }
                catch (Exception e)
                {
                    //Assert false
                    Debug.WriteLine(e.Message);
                    Assert.IsTrue(false);                
                }     
                
                //Assert
                try
                {
                    var userAccount = await userAccountRepo.GetUserAccountById(userId);
                    var id = userAccount.Id;
                    Debug.WriteLine(id);
                    Assert.IsTrue(false);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);

                    if (e.Message.Contains("reference"))
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }                      
                }

            }

            [DataTestMethod]
            [DataRow(3)]
            public async Task DeleteProfile_ProfileExists_ProfileDeleted(int userId)
            {
                // Arrange
                var userProfileRepo = new UserProfileRepository(new DataGateway(), new ConnectionStringData());

                // Act
                try
                {
                    await DeletionService.DeleteProfile(userId);
                }
                catch (Exception e)
                {
                    //Assert false
                    Debug.WriteLine(e.Message);
                    Assert.IsTrue(false);
                }

                //Assert
                try
                {
                    var userProfile = await userProfileRepo.GetUserProfileByAccountId(userId);
                    var id = userProfile.Id;
                    Debug.WriteLine(id);
                    Assert.IsTrue(false);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);

                    if (e.Message.Contains("reference"))
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }
                }

            }

        }
    }
}
