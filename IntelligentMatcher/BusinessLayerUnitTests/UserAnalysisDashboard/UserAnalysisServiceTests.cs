using DataAccess;
using DataAccess.Repositories;
using DataAccessUnitTestes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace BusinessLayerUnitTests.UserAnalysisDashboard
{
    [TestClass]
    public class UserAnalysisServiceTests
    {
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                UserAccountModel userAccountModel = new UserAccountModel();
                userAccountModel.Id = i;
                userAccountModel.Username = "TestUser" + i;
                userAccountModel.Password = "TestPassword" + i;
                userAccountModel.Salt = "TestSalt" + i;
                userAccountModel.EmailAddress = "TestEmailAddress" + i;
                userAccountModel.AccountType = "TestAccountType" + i;
                userAccountModel.AccountStatus = "TestAccountStatus";
                userAccountModel.CreationDate = DateTimeOffset.UtcNow;
                userAccountModel.UpdationDate = DateTimeOffset.UtcNow;

                await userAccountRepository.CreateAccount(userAccountModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            var accounts = await userAccountRepository.GetAllAccounts();

            foreach (var account in accounts)
            {
                await userAccountRepository.DeleteAccountById(account.Id);
            }
            await DataAccessTestHelper.ReseedAsync("UserAccount", 0, connectionString, dataGateway);
        }
        [DataTestMethod]
        [DataRow("Suspended")]
        public async Task TrackSuspendedAccounts_Countaccounts(string status)
        {
            int expected = 20;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IEnumerable<UserAccountModel> models = await userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                model.AccountStatus = "Suspended";
                await userAccountRepository.UpdateAccountStatus(model.Id, model.AccountStatus);
                if (model.AccountStatus == "Suspended")
                {
                    userAccountModels.Add(model);
                }

            }
            var count = userAccountModels.Count();
            if (expected == count)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }

        [DataTestMethod]
        [DataRow("Banned")]
        public async Task TrackBannedAccounts_Countaccounts(string status)
        {
            int expected = 20;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IEnumerable<UserAccountModel> models = await userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                model.AccountStatus = "Banned";
                await userAccountRepository.UpdateAccountStatus(model.Id, model.AccountStatus);
                if (model.AccountStatus == "Banned")
                {
                    userAccountModels.Add(model);
                }

            }
            var count = userAccountModels.Count();
            if (expected == count)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }
        [DataTestMethod]
        [DataRow("Shadow-Banned")]
        public async Task TrackShadowBannedAccounts_Countaccounts(string status)
        {
            int expected = 20;
            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IUserAccountRepository userAccountRepository = new UserAccountRepository(dataGateway, connectionString);
            IEnumerable<UserAccountModel> models = await userAccountRepository.GetAllAccounts();
            List<UserAccountModel> userAccountModels = new List<UserAccountModel>();
            foreach (var model in models)
            {
                model.AccountStatus = "Shadow-Banned";
                await userAccountRepository.UpdateAccountStatus(model.Id, model.AccountStatus);
                if (model.AccountStatus == "Shadow-Banned")
                {
                    userAccountModels.Add(model);
                }

            }
            var count = userAccountModels.Count();
            if (expected == count)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }

        }



    


    

    }//end of class
  

  
}
