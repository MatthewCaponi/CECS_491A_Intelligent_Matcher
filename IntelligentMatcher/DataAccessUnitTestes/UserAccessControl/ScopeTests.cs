using DataAccess;
using DataAccess.Repositories.User_Access_Control.EntitlementManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.User_Access_Control;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestHelper;

namespace DataAccessUnitTestes.UserAccessControl
{
    [TestClass]
    public class ScopeTests
    {
        #region Test Setup
        // Insert test rows before every test case
        [TestInitialize()]
        public async Task Init()
        {
            await TestCleaner.CleanDatabase();
            var numTestRows = 20;

            IDataGateway dataGateway = new SQLServerGateway();
            IConnectionStringData connectionString = new ConnectionStringData();
            IScopeRepository scopeRepository = new ScopeRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                ScopeModel scopeModel = new ScopeModel();
                scopeModel.Id = i;
                scopeModel.Type = "TestScope" + i;
                scopeModel.Description = "TestDescription" + i;
                scopeModel.IsDefault = true;

                await scopeRepository.CreateScope(scopeModel);
            }
        }

        // Remove test rows and reset id counter after every test case
        [TestCleanup()]
        public async Task CleanUp()
        {
            await TestCleaner.CleanDatabase();
        }

        #endregion
        #region Functional Tests
        [TestMethod]
        public async Task GetScopes_ScopesExist_ScopeIdIsAccurate()
        {
            // Arrange
            IScopeRepository scopeRepository = new ScopeRepository(new SQLServerGateway(), new ConnectionStringData());

            // Act
            var scopes = await scopeRepository.GetAllScopes();

            // Assert
            int i = 1;
            foreach (var scope in scopes)
            {
                if (scope.Id == i)
                {
                    ++i;
                    continue;
                }

                Assert.IsTrue(false);
                return;
            }

            Assert.IsTrue(true);
        }

        [DataTestMethod]
        [DataRow(1, "UpdatedScopeName", "UpdatedDescriptionName", false)]
        public async Task UpdateScope_ScopeExists_ScopeNameIsAccurate(int id, string expectedUpdatedName, string expectedDescriptionName, bool expectedDefaultValue)
        {
            // Arrange
            IScopeRepository scopeRepository = new ScopeRepository(new SQLServerGateway(), new ConnectionStringData());
            ScopeModel scopeModel = new ScopeModel();
            scopeModel.Id = id;
            scopeModel.Type = expectedUpdatedName;
            scopeModel.Description = expectedDescriptionName;
            scopeModel.IsDefault = expectedDefaultValue;

            // Act
            await scopeRepository.UpdateScope(scopeModel);
            var actual = await scopeRepository.GetScopeById(id);

            var actualName = actual.Type;
            var actualDescriptionName = actual.Description;
            var actualDefaultValue = actual.IsDefault;


            // Assert
            Assert.IsTrue(actualName == expectedUpdatedName &&
                          actualDescriptionName == expectedDescriptionName &&
                          actualDefaultValue == expectedDefaultValue);
        }
        #endregion
    }
}
