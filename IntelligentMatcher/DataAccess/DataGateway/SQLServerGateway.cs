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
using static Models.UserProfileModel;

namespace DataAccess
{
    public class SQLServerGateway : IDataGateway
    {
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
                return null;
            }
    
        }

        public async Task<int> SaveData<T>(string storedProcedure, T parameters, string connectionString)
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
                if (e.Message.Contains("duplicate") && e.Message.Contains("Username"))
                {
                    throw new Exception("Username already exists", e.InnerException);
                }
                else if (e.Message.Contains("duplicate") && e.Message.Contains("EmailAddress"))
                {
                    throw new Exception("Email already exists", e.InnerException);
                }
                else
                {
                    throw new Exception(e.Message);
                }                                 
            }                 
        }
    }
}
