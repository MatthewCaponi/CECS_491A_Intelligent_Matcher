using DataAccess;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ConsoleUI
{
    public class TestDataAcces
    {

        private static IDataGateway dataGateway = new DataGateway();
        private static IConnectionStringData connectionStringData = new ConnectionStringData();
        private static IUserAccountRepository userAccount;
        public static void Main()
        {

            userAccount = new UserAccountRepository(dataGateway, connectionStringData);
            UserAccountModel model = new UserAccountModel();
            model.Username = "Matt014";
            model.Password = "lalalalala";
            model.EmailAddress = "mattc@gmail.com";
            userAccount.CreateUserAccount(model);

        }
        
    }
}
