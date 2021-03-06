﻿using Dapper;
using Exceptions;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AccountVerificationRepo : IAccountVerificationRepo
    {



        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;
        public AccountVerificationRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }



        public async Task<int> CreateAccountVerification(int userId, string token)
        {
            try
            {
                var storedProcedure = "dbo.AccountVerification_Create";

                DynamicParameters p = new DynamicParameters();

                p.Add("UserId", userId);
                p.Add("Token", token);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

                return p.Get<int>("Id");
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<string> GetStatusTokenByUserId(int userId)
        {
            try
            {
                var storedProcedure = "dbo.AccountVerification_GetStatusToken_ByUserId";

                var row = await _dataGateway.LoadData<string, dynamic>(storedProcedure,
                    new
                    {
                        UserId = userId
                    },
                    _connectionString.SqlConnectionString);

                return row.FirstOrDefault();
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> UpdateAccountStatusToken(int userId, string token)
        {
            try
            {
                var storedProcedure = "dbo.AccountVerification_Update_AccountStatusToken";

                return await _dataGateway.Execute(storedProcedure,
                                             new
                                             {
                                                 Token = token,
                                                 UserId = userId
                                             },
                                             _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }


        public async Task<int> DeleteAccountVerificationById(int id)
        {
            try
            {
                var storedProcedure = "dbo.AccountVerification_Delete_ById";

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

        public async Task<IEnumerable<VerficationTokenModel>> GetAllAccountVerifications()
        {
            try
            {
                string storedProcedure = "dbo.AccountVerification_Get_All";


                return await _dataGateway.LoadData<VerficationTokenModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);
            }
            catch (SqlCustomException e)
            {
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

    }
}
