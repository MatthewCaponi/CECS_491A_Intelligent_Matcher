using DataAccess;
using DataAccess.Repositories;
using Models;
using System;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        private static IDataGateway dataGateway = new DataGateway();
        private static IConnectionStringData connectionStringData = new ConnectionStringData();
        private static IUserAccountRepository userAccount;
         static async Task Main()
        {

             await GetUser();
            
        }

        public static async Task GetUser()
        {
            userAccount = new UserAccountRepository(dataGateway, connectionStringData);
            UserAccountModel model = new UserAccountModel();
            model.Username = "Matt014";
            model.Password = "lalalalala";
            model.EmailAddress = "mattc@gmail.com";
           // System.Console.WriteLine("Adding user");
            
            //model = await userAccount.GetUserAccountById(2);
            //System.Console.WriteLine("User Info: " + model.Username);

            System.Console.WriteLine("Adding User");
            await userAccount.CreateUserAccount(model);
        }
    }
}
