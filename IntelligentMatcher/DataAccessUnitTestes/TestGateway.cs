using Dapper;
using DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessUnitTestes
{
    public class TestGateway : IDataGateway
    {
        public async Task<List<T>> LoadData<T, U>(string query, U parameters, string connectionString)
        {
            using (TransactionScope scope = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var rows = await connection.QueryAsync<T>(query,
                                                          parameters);

                    return rows.ToList();

                }
                catch (Exception e)
                {

                }

                return null;             
            }
        }

        public async Task<int> SaveData<T>(string query, T parameters, string connectionString)
        {
            using (TransactionScope scope = new TransactionScope())
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var id = await connection.ExecuteAsync(query,
                                                     parameters);

                    return id;
                }
                catch (Exception e)
                {

                }

                return 0;
                
            }
        }
    }
}
