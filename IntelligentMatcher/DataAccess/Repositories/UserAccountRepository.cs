using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Models;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using Exceptions;
using Logging;

namespace DataAccess.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;
        private readonly ILogService _logService;

        public UserAccountRepository(IDataGateway dataGateway, IConnectionStringData connectionString, ILogService logService)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
            _logService = logService;
        }
 
        public async Task<IEnumerable<UserAccountModel>> GetAllAccounts()
        {
            string storedProcedure = "dbo.UserAccount_Get_All";

            try
            {
                return await _dataGateway.LoadData<UserAccountModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return null;
            }
        }

        public async Task<UserAccountModel> GetAccountById(int id)
        {
            string storedProcedure = "dbo.UserAccount_Get_ById";

            try
            {
                var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return null;
            }
        }

        public async Task<UserAccountModel> GetAccountByUsername(string username)
        {
            var storedProcedure = "dbo.UserAccount_Get_ByUsername";

            try
            {
                var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(storedProcedure,
                new
                {
                    Username = username
                },
                _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return null;
            }
        }

        public async Task<UserAccountModel> GetAccountByEmail(string email)
        {
            var storedProcedure = "dbo.UserAccount_Get_ByEmail";

            try
            {
                var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(storedProcedure,
               new
               {
                   EmailAddress = email
               },
               _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return null;
            }
        }

        public async Task<string> GetSaltById(int id)
        {
            var storedProcedure = "dbo.UserAccount_GetSalt_ById";

            try
            {
                var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return null;
            }
        }
        public async Task<string> GetPasswordById(int id)
        {
            var storedProcedure = "dbo.UserAccount_GetPassword_ById";

            try
            {
                var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return null;
            }
        }

        public async Task<string> GetStatusById(int id)
        {
            var storedProcedure = "dbo.UserAccount_GetStatus_ById";

            try
            {
                var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return null;
            }
        }

        public async Task<int?> CreateAccount(UserAccountModel model)
        {
            var storedProcedure = "dbo.UserAccount_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("Username", model.Username);
            p.Add("Password", model.Password);
            p.Add("Salt", model.Salt);
            p.Add("EmailAddress", model.EmailAddress);
            p.Add("AccountType", model.AccountType);
            p.Add("AccountStatus", model.AccountStatus);
            p.Add("CreationDate", model.CreationDate);
            p.Add("UpdationDate", model.UpdationDate);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

                return p.Get<int>("Id");
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return null;
            }

        }

        public async Task<bool> DeleteAccountById(int id)
        {
            var storedProcedure = "dbo.UserAccount_Delete_ById";
  
            try
            {
                await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);

                return true;
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return false;
            }

        }

        public async Task<bool> UpdateAccountUsername(int id, string username)
        {
            var storedProcedure = "dbo.UserAccount_Update_Username";

            try
            {
                await _dataGateway.Execute(storedProcedure,
                                          new
                                          {
                                              Id = id,
                                              Username = username
                                          },
                                          _connectionString.SqlConnectionString);

                return true;
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return false;
            }
        }

        public async Task<bool> UpdateAccountEmail(int id, string email)
        {
            var storedProcedure = "dbo.UserAccount_Update_Email";

            try
            {
                await _dataGateway.Execute(storedProcedure,
                                        new
                                        {
                                            Id = id,
                                            EmailAddress = email
                                        },
                                        _connectionString.SqlConnectionString);

                return true;
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return false;
            }
        }

        public async Task<bool> UpdateAccountPassword(int id, string password)
        {
            var storedProcedure = "dbo.UserAccount_Update_Password";

            try
            {
                await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id,
                                             Password = password
                                         },
                                         _connectionString.SqlConnectionString);

                return true;
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return false;
            }
        }

        public async Task<bool> UpdateAccountSalt(int id, string salt)
        {
            var storedProcedure = "dbo.UserAccount_Update_Salt";

            try
            {
                await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id,
                                             Salt = salt
                                         },
                                         _connectionString.SqlConnectionString);

                return true;
            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return false;
            }
        }


        public async Task<bool> UpdateAccountStatus(int id, string accountStatus)
        {
            var storedProcedure = "dbo.UserAccount_Update_AccountStatus";

            try
            {
                await _dataGateway.Execute(storedProcedure,
                                 new
                                 {
                                     AccountStatus = accountStatus,
                                     Id = id
                                 },
                                 _connectionString.SqlConnectionString);

                return true;

            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return false;
            }
        }

        public async Task<bool> UpdateAccountType(int id, string accountType)
        {
            var storedProcedure = "dbo.UserAccount_Update_AccountType";

            try
            {
                await _dataGateway.Execute(storedProcedure,
                                          new
                                          {
                                              AccountType = accountType,
                                              Id = id

                                          },
                                          _connectionString.SqlConnectionString);

                return true;

            }
            catch (SqlCustomException e)
            {
                _logService.Log("Caught SqlCustomExceptionException", LogTarget.All, LogLevel.error, e, this.ToString(), "Database_Logs");
                return false;
            }
        }
    }
}
