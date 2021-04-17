using DataAccess;
using DataAccess.Repositories;
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
    public class AssignmentPolicyTests
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
            IAssignmentPolicyRepository assignmentPolicyRepository = new AssignmentPolicyRepository(dataGateway, connectionString);

            for (int i = 1; i <= numTestRows; ++i)
            {
                AssignmentPolicyModel assignmentPolicyModel = new AssignmentPolicyModel();
                assignmentPolicyModel.Id = i;
                assignmentPolicyModel.Name = "TestClaim" + i;
                assignmentPolicyModel.IsDefault = true;
                assignmentPolicyModel.RequiredAccountType = "TestAccountType" + i;
                assignmentPolicyModel.Priority = i % 4;

                await assignmentPolicyRepository.CreateAssignmentPolicy(assignmentPolicyModel);
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
        [DataRow(3, "TestUpdatedName", false, "TestUpdatedAccountType", 0)]
        public async Task UpdateAssignmentPolicy_AssignmentPolicyExists_AssignmentPolicyValuesAccurate(int id, string expectedUpdatedName, bool expectedDefaultValue, 
            string expectedRequiredAccountType, int expectedPriority)
        {
            // Arrange
            IAssignmentPolicyRepository assignmentPolicyRepository = new AssignmentPolicyRepository(new SQLServerGateway(), new ConnectionStringData());
            AssignmentPolicyModel assignmentPolicyModel = new AssignmentPolicyModel();
            assignmentPolicyModel.Id = id;
            assignmentPolicyModel.Name = expectedUpdatedName;
            assignmentPolicyModel.IsDefault = expectedDefaultValue;
            assignmentPolicyModel.RequiredAccountType = expectedRequiredAccountType;
            assignmentPolicyModel.Priority = expectedPriority;

            // Act
            await assignmentPolicyRepository.UpdateAssignmentPolicy(assignmentPolicyModel);
            var actual = await assignmentPolicyRepository.GetAssignmentPolicyById(id);

            var actualName = actual.Name;
            var actualDefaultValue = actual.IsDefault;
            var actualRequiredAccountType = actual.RequiredAccountType;
            var actualPriority = actual.Priority;


            // Assert
            Assert.IsTrue(actualName == expectedUpdatedName &&
                          actualDefaultValue == expectedDefaultValue &&
                          actualRequiredAccountType == expectedRequiredAccountType &&
                          actualPriority == expectedPriority);
        }
        #endregion
    }
}
