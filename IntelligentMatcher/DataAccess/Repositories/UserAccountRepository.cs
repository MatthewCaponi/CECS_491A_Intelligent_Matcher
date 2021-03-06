using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Models;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using Exceptions;

namespace DataAccess.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserAccountRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }
 
        public async Task<IEnumerable<UserAccountModel>> GetAllAccounts()
        {
            try
            {
                string storedProcedure = "dbo.UserAccount_Get_All";

                return await _dataGateway.LoadData<UserAccountModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<UserAccountModel> GetAccountById(int id)
        {
            try
            {
                string storedProcedure = "dbo.UserAccount_Get_ById";

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
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<UserAccountModel> GetAccountByUsername(string username)
        {
            try
            {
                var storedProcedure = "dbo.UserAccount_Get_ByUsername";

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
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<UserAccountModel> GetAccountByEmail(string email)
        {
            try
            {
                var storedProcedure = "dbo.UserAccount_Get_ByEmail";

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
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<string> GetSaltById(int id)
        {
            try
            {
                var storedProcedure = "dbo.UserAccount_GetSalt_ById";

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
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<string> GetPasswordById(int id)
        {
            try
            {
                var storedProcedure = "dbo.UserAccount_GetPassword_ById";

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
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }



        public async Task<string> GetStatusById(int id)
        {
            try
            {
                var storedProcedure = "dbo.UserAccount_GetStatus_ById";

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
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> CreateAccount(UserAccountModel model)
        {
            try
            {
                if(model.Username.Length <= 50 && model.EmailAddress.Length <= 50)
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

                    await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

                    return p.Get<int>("Id");
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> DeleteAccountById(int id)
        {
            try
            {
                var storedProcedure = "dbo.UserAccount_Delete_ById";

                return await _dataGateway.Execute(storedProcedure,
                                             new
                                             {
                                                 Id = id
                                             },
                                             _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> UpdateAccountUsername(int id, string username)
        {
            try
            {
                if(username.Length <= 50)
                {
                    var storedProcedure = "dbo.UserAccount_Update_Username";

                    return await _dataGateway.Execute(storedProcedure,
                                                 new
                                                 {
                                                     Id = id,
                                                     Username = username
                                                 },
                                                 _connectionString.SqlConnectionString);
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> UpdateAccountEmail(int id, string email)
        {
            try
            {
                if (email.Length <= 50)
                {
                    var storedProcedure = "dbo.UserAccount_Update_Email";

                    return await _dataGateway.Execute(storedProcedure,
                                                 new
                                                 {
                                                     Id = id,
                                                     EmailAddress = email
                                                 },
                                                 _connectionString.SqlConnectionString);
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> UpdateAccountPassword(int id, string password)
        {
            try
            {
                if (password.Length <= 50)
                {
                    var storedProcedure = "dbo.UserAccount_Update_Password";

                    return await _dataGateway.Execute(storedProcedure,
                                                 new
                                                 {
                                                     Id = id,
                                                     Password = password
                                                 },
                                                 _connectionString.SqlConnectionString);
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> UpdateAccountSalt(int id, string salt)
        {
            try
            {
                if (salt.Length <= 100)
                {
                    var storedProcedure = "dbo.UserAccount_Update_Salt";

                    return await _dataGateway.Execute(storedProcedure,
                                                 new
                                                 {
                                                     Id = id,
                                                     Salt = salt
                                                 },
                                                 _connectionString.SqlConnectionString);
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }


        public async Task<int> UpdateAccountStatus(int id, string accountStatus)
        {
            try
            {
                if (accountStatus.Length <= 50)
                {
                    var storedProcedure = "dbo.UserAccount_Update_AccountStatus";

                    return await _dataGateway.Execute(storedProcedure,
                                                 new
                                                 {
                                                     AccountStatus = accountStatus,
                                                     Id = id
                                                 },
                                                 _connectionString.SqlConnectionString);
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> UpdateAccountType(int id, string accountType)
        {
            try
            {
                if (accountType.Length <= 50)
                {
                    var storedProcedure = "dbo.UserAccount_Update_AccountType";

                    return await _dataGateway.Execute(storedProcedure,
                                                 new
                                                 {
                                                     AccountType = accountType,
                                                     Id = id

                                                 },
                                                 _connectionString.SqlConnectionString);
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }
    }
}
