using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataGateway : IDataGateway
    {

        public async Task<List<T>> LoadData<T, U>(string query, U parameters, string connectionString)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    var rows = await connection.QueryAsync<T>(query,
                                                              parameters);

                    return rows.ToList();
                }
            }
            catch (SqlException e)
            {
                //based on error number
                throw new UserNotFoundException("User not found");   
                return null;
            }
            
        }

        public async Task<int> SaveData<T>(string query, T parameters, string connectionString)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    return await connection.ExecuteAsync(query,
                                                         parameters);

                }
            }
            catch (SqlException e)
            {
                return 0;
            }
            
        }

    }
}
