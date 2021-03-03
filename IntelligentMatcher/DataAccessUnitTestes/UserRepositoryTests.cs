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
    //[TestClass]
    //public class UserRepositoryTests
    //{
    //    [TestInitialize()]
    //    public async Task Init()
    //    {
    //        var numTestRows = 10;

    //        IDataGateway dataGateway = new DataGateway();
    //        IConnectionStringData connectionString = new ConnectionStringData();
    //        ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

    //        for (int i = 1; i <= numTestRows; ++i)
    //        {
    //            UserAccountModel userAccountModel = new UserAccountModel("TestUser" + i, "TestPass" + i, "TestEmail" + i);
    //            userAccountModel.Id = await testRepo.InsertUserAccountTestRows(userAccountModel);

    //            UserProfileModel model = new UserProfileModel($"FirstName{i}", $"LastName{i}", DateTime.Now, DateTime.Today.Date,
    //                 UserProfileModel.AccountType.User.ToString(), UserProfileModel.AccountStatus.Active.ToString(), userAccountModel.Id);
    //            await testRepo.InsertUserProfileTestRows(model);

    //        }
    //    }

    //    [TestCleanup()]
    //    public async Task CleanUp()
    //    {
    //        IDataGateway dataGateway = new DataGateway();
    //        IConnectionStringData connectionString = new ConnectionStringData();
    //        ITestRepo testRepo = new TestRepo(dataGateway, connectionString);

    //        await testRepo.DeleteUserAccountTestRows();
    //        await testRepo.DeleteUserProfileTestRows();
    //    }

    //    [DataTestMethod]
    //    public async Task GetUserList()
    //    {
    //        // Arrange
    //        IUserRepository user = new UserRepository(new DataGateway(), new ConnectionStringData());


    //        // Act
    //        List<UserListTransferModel> userList = new List<UserListTransferModel>();
    //        userList = await user.GetUserList();

    //        // Assert
    //       for (int i = 0; i < 10; ++i)
    //        {
    //            if (userList[i].Id != (i + 1))
    //            {
    //                Assert.IsTrue(false);
    //            }
    //        }

    //        Assert.IsTrue(true);
                
    //    }
    //}
}
