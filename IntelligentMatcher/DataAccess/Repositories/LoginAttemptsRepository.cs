﻿using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class LoginAttemptsRepository : ILoginAttemptsRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public LoginAttemptsRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> CreateLoginAttempts(LoginAttemptsModel model)
        {
            var storedProcedure = "dbo.LoginAttempts_Create";
            DynamicParameters p = new DynamicParameters();

            p.Add("IpAddress", model.IpAddress);
            p.Add("LoginCounter", model.LoginCounter);
            p.Add("SuspensionEndTime", model.SuspensionEndTime);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            return await _dataGateway.SaveData(storedProcedure, p, _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteLoginAttemptsById(int id)
        {
            var storedProcedure = "dbo.LoginAttempts_Delete_ById";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<LoginAttemptsModel>> GetAllLoginAttempts()
        {
            string storedProcedure = "dbo.LoginAttempts_Get_All";

            return await _dataGateway.LoadData<LoginAttemptsModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<LoginAttemptsModel> GetLoginAttemptsById(int id)
        {
            string storedProcedure = "dbo.LoginAttempts_Get_ById";

            var row = await _dataGateway.LoadData<LoginAttemptsModel, dynamic>(storedProcedure,
                new
                {
                    Id = id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<LoginAttemptsModel> GetLoginAttemptsByIpAddress(string ipAddress)
        {
            string storedProcedure = "dbo.LoginAttempts_Get_ByIpAddress";

            var row = await _dataGateway.LoadData<LoginAttemptsModel, dynamic>(storedProcedure,
                new
                {
                    IpAddress = ipAddress
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }

        public async Task<int> IncrementLoginCounterById(int id)
        {
            var storedProcedure = "dbo.LoginAttempts_IncrementLoginCounter_ById";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> IncrementLoginCounterByIpAddress(string ipAddress)
        {
            var storedProcedure = "dbo.LoginAttempts_IncrementLoginCounter_ByIpAddress";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             IpAddress = ipAddress
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> ResetLoginCounterById(int id)
        {
            var storedProcedure = "dbo.LoginAttempts_ResetLoginCounter_ById";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> ResetLoginCounterByIpAddress(string ipAddress)
        {
            var storedProcedure = "dbo.LoginAttempts_ResetLoginCounter_ByIpAddress";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             IpAddress = ipAddress
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateSuspensionEndTimeById(int id, DateTimeOffset suspensionEndTime)
        {
            var storedProcedure = "dbo.LoginAttempts_UpdateSuspensionEndTime_ById";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             Id = id,
                                             SuspensionEndTime = suspensionEndTime
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateSuspensionEndTimeByIpAddress(string ipAddress, DateTimeOffset suspensionEndTime)
        {
            var storedProcedure = "dbo.LoginAttempts_UpdateSuspensionEndTime_ByIpAddress";

            return await _dataGateway.SaveData(storedProcedure,
                                         new
                                         {
                                             IpAddress = ipAddress,
                                             SuspensionEndTime = suspensionEndTime
                                         },
                                         _connectionString.SqlConnectionString);
        }
    }
}