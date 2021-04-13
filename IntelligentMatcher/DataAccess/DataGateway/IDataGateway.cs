using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataGateway
    { 
        Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionString);
        Task<int> Execute<T>(string storedProcedure, T parameters, string connectionString);
    }
}