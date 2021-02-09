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
    public class DataGateway : IDataGateway
    {
        private ILogService _logger;
        public DataGateway()
        {
            ILogServiceFactory factory = new LogSeviceFactory();
            factory.AddTarget(TargetType.Text);

            _logger = factory.CreateLogService<DataGateway>();
        }

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
                _logger.LogError(new UserLoggingEvent(EventName.UserEvent, "", 0, AccountType.User), e, $"Exception: {e.Message}");
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
                if (e.Message.Contains("duplicate") && e.Message.Contains("Username"))
                {
                    _logger.LogError(new UserLoggingEvent(EventName.UserEvent, "", 0, AccountType.User), e, $"Exception: {e.Message}");
                    throw new Exception("Username already exists", e.InnerException);
                }
                else if (e.Message.Contains("duplicate") && e.Message.Contains("EmailAddress"))
                {
                    _logger.LogError(new UserLoggingEvent(EventName.UserEvent, "", 0, AccountType.User), e, $"Exception: {e.Message}");
                    throw new Exception("Email already exists", e.InnerException);
                }
                else
                {
                    _logger.LogError(new UserLoggingEvent(EventName.UserEvent, "", 0, AccountType.User), e, $"Exception: {e.Message}");
                    throw new Exception("SqlException", e.InnerException);
                }    
                                  
            }                 
        }

    }
}
