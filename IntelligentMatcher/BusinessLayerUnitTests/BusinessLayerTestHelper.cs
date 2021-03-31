using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Models;

namespace BusinessLayerUnitTests
{
    public class BusinessLayerTestHelper
    {
        public List<WebUserAccountModel> PopulateListOfAccountModels(int numAccounts, string namePrefix, 
            int startingId, int idIncrementFactor)
        {
            List<WebUserAccountModel> accounts = new List<WebUserAccountModel>();
            DateTimeOffset testDate = DateTimeOffset.UtcNow;

            for (int i = startingId; i < numAccounts; i += idIncrementFactor)
            {
                
                WebUserAccountModel webUserAccountModel = new WebUserAccountModel();
                webUserAccountModel.Id = i;
                webUserAccountModel.Username = $"{namePrefix}Username{i}";
                webUserAccountModel.EmailAddress = $"{namePrefix}EmailAddress{i}";
                webUserAccountModel.AccountType = $"{namePrefix}{AccountType.User}{i}";
                webUserAccountModel.AccountStatus = $"{namePrefix}{AccountStatus.Active}{i}";
                webUserAccountModel.CreationDate = testDate;
                webUserAccountModel.UpdationDate = testDate;

                
                accounts.Add(webUserAccountModel);
            }

            return accounts;
        }
    }
}
