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
    public class AssignmentPolicyPairingTest
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
            IAssignmentPolicyRepository assignmentPolicyRepository = new AssignmentPolicyRepository(dataGateway, connectionString);
            IAssignmentPolicyPairingRepository assignmentPolicyPairingRepository = new AssignmentPolicyPairingRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                ScopeModel scopeModel = new ScopeModel();
                scopeModel.Id = i;
                scopeModel.Name = "TestScope" + i;
                scopeModel.Description = "TestDescription" + i;

                scopeModel.IsDefault = true;
                AssignmentPolicyModel assignmentPolicyModel = new AssignmentPolicyModel();
                assignmentPolicyModel.Id = i;
                assignmentPolicyModel.Name = "TestClaim" + i;
                assignmentPolicyModel.IsDefault = true;
                assignmentPolicyModel.RequiredAccountType = "TestAccountType" + i;
                assignmentPolicyModel.Priority = i % 4;

                AssignmentPolicyPairingModel assignmentPolicyPairingModel = new AssignmentPolicyPairingModel();
                assignmentPolicyPairingModel.Id = i;
                assignmentPolicyPairingModel.PolicyId = i;
                assignmentPolicyPairingModel.ScopeId = i;

                await scopeRepository.CreateScope(scopeModel);
                await assignmentPolicyRepository.CreateAssignmentPolicy(assignmentPolicyModel);
                await assignmentPolicyPairingRepository.CreateAssignmentPolicyPairing(assignmentPolicyPairingModel);
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
        [DataTestMethod]
        [DataRow(3, 1, 2)]
        public async Task UpdateAssignmentPolicyPairing_AssignmentPolicyPairingExists_AssignmentPolicyPairingValuesAccurate(int id, int expectedPolicyId, int expectedScopeId)
        {
            // Arrange
            IAssignmentPolicyPairingRepository assignmentPolicyPairingRepository = new AssignmentPolicyPairingRepository(new SQLServerGateway(), new ConnectionStringData());
            AssignmentPolicyPairingModel assignmentPolicyPairingModel = new AssignmentPolicyPairingModel();
            assignmentPolicyPairingModel.Id = id;
            assignmentPolicyPairingModel.PolicyId = expectedPolicyId;
            assignmentPolicyPairingModel.ScopeId = expectedScopeId;

            // Act
            await assignmentPolicyPairingRepository.UpdateAssignmentPolicyPairing(assignmentPolicyPairingModel);
            var actual = await assignmentPolicyPairingRepository.GetAssignmentPolicyPairingById(id);

            var actualPolicyId = actual.PolicyId;
            var actualScopeId = actual.ScopeId;


            // Assert
            Assert.IsTrue(actualPolicyId == expectedPolicyId &&
                          actualScopeId == expectedScopeId);
        }
        #endregion
    }
}
