using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Models;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using Exceptions;
using DataAccessLayer.CrossCuttingConcerns;

namespace DataAccess.Repositories
{
    public class UserAccountRepository : DataAccessBase, IUserAccountRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserAccountRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }
 
        public async Task<Result<IEnumerable<UserAccountModel>>> GetAllAccounts()
        {
            string storedProcedure = "dbo.UserAccount_Get_All";
            try
            {
                var accounts = await _dataGateway.LoadData<UserAccountModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);

                return Result<IEnumerable<UserAccountModel>>.Success(accounts);
            }
            catch (SqlCustomException e)
            {
                return Result<IEnumerable<UserAccountModel>>.Failure(e.ToString());
            }
        }

        public async Task<Result<UserAccountModel>> GetAccountById(int id)
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

                return Result<UserAccountModel>.Success(row.FirstOrDefault());
            }
            catch (SqlCustomException e)
            {
                return Result<UserAccountModel>.Failure(e.ToString());
            }
        }

        public async Task<Result<UserAccountModel>> GetAccountByUsername(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                return Result<UserAccountModel>.Failure(ErrorMessage.NullOrEmpty.ToString());
            }

            var storedProcedure = "dbo.UserAccount_Get_ByUsername";

            try
            {
                var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(storedProcedure,
                    new
                    {
                        Username = username
                    },
                    _connectionString.SqlConnectionString);

                return Result<UserAccountModel>.Success(row.FirstOrDefault());
            }
            catch (SqlCustomException e)
            {
                return Result<UserAccountModel>.Failure(e.ToString());
            }
        }

        public async Task<Result<UserAccountModel>> GetAccountByEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return Result<UserAccountModel>.Failure(ErrorMessage.NullOrEmpty.ToString());
            }

            var storedProcedure = "dbo.UserAccount_Get_ByEmail";

            try
            {
                var row = await _dataGateway.LoadData<UserAccountModel, dynamic>(storedProcedure,
                    new
                    {
                        EmailAddress = email
                    },
                    _connectionString.SqlConnectionString);

                return Result<UserAccountModel>.Success(row.FirstOrDefault());
            }
            catch (SqlCustomException e)
            {
                return Result<UserAccountModel>.Failure(e.ToString());
            }
        }

        public async Task<Result<string>> GetSaltById(int id)
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

                return Result<string>.Success(row.FirstOrDefault());
            }
            catch (SqlCustomException e)
            {
                return Result<string>.Failure(e.ToString());
            }
        }

        public async Task<Result<string>> GetPasswordById(int id)
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

                return Result<string>.Success(row.FirstOrDefault());
            }
            catch (SqlCustomException e)
            {
                return Result<string>.Failure(e.ToString());
            }
        }



        public async Task<Result<string>> GetStatusById(int id)
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

                return Result<string>.Success(row.FirstOrDefault());
            }
            catch (SqlCustomException e)
            {
                return Result<string>.Failure(e.ToString());
            }
        }

        public async Task<Result<int>> CreateAccount(UserAccountModel model)
        {
            if (!InvalidLength(model, 50))
            {
                return Result<int>.Failure(ErrorMessage.InvalidLength.ToString());
            }

            if (!IsNull(model))
            {
                return Result<int>.Failure(ErrorMessage.NullObject.ToString());
            }

            if (!ContainsNullOrEmptyParameter(model, new List<string> { nameof(model.Password), nameof(model.Salt) }))
            {
                return Result<int>.Failure(ErrorMessage.NullOrEmptyParameter.ToString());
            }

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

                return Result<int>.Success(p.Get<int>("Id"));
            }
            catch (SqlCustomException e)
            {
                return Result<int>.Failure(e.ToString());
            }
        }

        public async Task<Result<bool>> DeleteAccountById(int id)
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

                return Result<bool>.Success(true);
            }
            catch (SqlCustomException e)
            {
                return Result<bool>.Failure(e.ToString());
            }
        }

        public async Task<Result<bool>> UpdateAccountUsername(int id, string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Result<bool>.Failure(ErrorMessage.NullOrEmpty.ToString());
            }
            if (InvalidLength(username, 50))
            {
                return Result<bool>.Failure(ErrorMessage.InvalidLength.ToString());
            }

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

                return Result<bool>.Success(true);
                
            }
            catch (SqlCustomException e)
            {
                return Result<bool>.Failure(e.ToString());
            }
        }

        public async Task<Result<bool>> UpdateAccountEmail(int id, string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return Result<bool>.Failure(ErrorMessage.NullOrEmpty.ToString());
            }
            if (InvalidLength(email, 50))
            {
                return Result<bool>.Failure(ErrorMessage.InvalidLength.ToString());
            }

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

                return Result<bool>.Success(true);

            }
            catch (SqlCustomException e)
            {
                return Result<bool>.Failure(e.ToString());
            }
        }

        public async Task<Result<bool>> UpdateAccountPassword(int id, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Result<bool>.Failure(ErrorMessage.NullOrEmpty.ToString());
            }
            if (InvalidLength(password, 50))
            {
                return Result<bool>.Failure(ErrorMessage.InvalidLength.ToString());
            }

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

                return Result<bool>.Success(true);

            }
            catch (SqlCustomException e)
            {
                return Result<bool>.Failure(e.ToString());
            }
        }

        public async Task<Result<bool>> UpdateAccountSalt(int id, string salt)
        {
            if (string.IsNullOrEmpty(salt))
            {
                return Result<bool>.Failure(ErrorMessage.NullOrEmpty.ToString());
            }
            if (InvalidLength(salt, 50))
            {
                return Result<bool>.Failure(ErrorMessage.InvalidLength.ToString());
            }

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

                return Result<bool>.Success(true);

            }
            catch (SqlCustomException e)
            {
                return Result<bool>.Failure(e.ToString());
            }
        }

        public async Task<Result<bool>> UpdateAccountStatus(int id, string accountStatus)
        {
            if (string.IsNullOrEmpty(accountStatus))
            {
                return Result<bool>.Failure(ErrorMessage.NullOrEmpty.ToString());
            }
            if (InvalidLength(accountStatus, 50))
            {
                return Result<bool>.Failure(ErrorMessage.InvalidLength.ToString());
            }

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

                return Result<bool>.Success(true);

            }
            catch (SqlCustomException e)
            {
                return Result<bool>.Failure(e.ToString());
            }
        }

        public async Task<Result<bool>> UpdateAccountType(int id, string accountType)
        {
            if (string.IsNullOrEmpty(accountType))
            {
                return Result<bool>.Failure(ErrorMessage.NullOrEmpty.ToString());
            }
            if (InvalidLength(accountType, 50))
            {
                return Result<bool>.Failure(ErrorMessage.InvalidLength.ToString());
            }

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

                return Result<bool>.Success(true);

            }
            catch (SqlCustomException e)
            {
                return Result<bool>.Failure(e.ToString());
            }
        }
    }
}
