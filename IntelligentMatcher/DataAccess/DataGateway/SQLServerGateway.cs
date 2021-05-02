using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;
using Exceptions;

namespace DataAccess
{
    public class SQLServerGateway : IDataGateway
    {
        private readonly ILogService _logService;

        public SQLServerGateway(ILogService logService)
        {
            _logService = logService;
        }
        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionString)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    var rows = await connection.QueryAsync<T>(storedProcedure,
                                                                parameters, commandType: CommandType.StoredProcedure);

                    return rows.ToList();
                }
            }
            catch (SqlException e)
            {
                _logService.Log("SqlException", LogTarget.All, LogLevel.error, this.ToString(), "Database_Logs");
                throw new SqlCustomException(e.Message, e.InnerException);
            }
        }

        public async Task<int> Execute<T>(string storedProcedure, T parameters, string connectionString)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    return await connection.ExecuteAsync(storedProcedure,
                                                            parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (SqlException e)
            {
                _logService.Log("SqlException", LogTarget.All, LogLevel.error, this.ToString(), "Database_Logs");
                throw new SqlCustomException(e.Message, e.InnerException);
            }                 
        }
    }
}
