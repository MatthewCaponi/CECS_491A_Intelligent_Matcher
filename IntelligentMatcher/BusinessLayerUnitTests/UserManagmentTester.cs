using DataAccess;
using DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayerUnitTests
{
    [TestClass]
    public class UserManagmentTester
    {
        [TestInitialize()]
        public void Init()
        {


        }

        [TestCleanup()]
        public void CleanUp()
        {


        }

      

        [DataTestMethod]
        [DataRow(1)]
        public void deleteUser(int id )
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserProfileRepository user = new UserProfileRepository(dataGateway, connectionString);

            user.DeleteUserProfileById(id);

        }

        [DataTestMethod]
        [DataRow(1, UserProfileModel.AccountStatus.Banned)]
        public async Task updateUserAccountStatusAsync(int id, UserProfileModel.AccountStatus accountStatus)
        {
            IDataGateway dataGateway = new DataGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserProfileRepository user = new UserProfileRepository(dataGateway, connectionString);

            user.UpdateUserAccountStatus(id, accountStatus);


            //var actualAccount = await user.GetUserProfileByAccountId(id);

            //Assert
            //Assert.IsTrue(actualAccount.accountStatus.ToString() == accountStatus.ToString());

        }

    }
    }
