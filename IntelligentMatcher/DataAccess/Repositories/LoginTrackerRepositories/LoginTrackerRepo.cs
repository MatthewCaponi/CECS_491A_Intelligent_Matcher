using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.LoginTrackerRepositories
{
    public class LoginTrackerRepo : ILoginTrackerRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public LoginTrackerRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DALLoginTrackerModel>> GetAllLogins()
        {
            string storedProcedure = "dbo.LoginTracker_GetALL";

            return await _dataGateway.LoadData<DALLoginTrackerModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> DeletebyId(int id)
        {
            var storedProcedure = "dbo.LoginTracker_DeletebyId";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> CreateLoginTracker(DALLoginTrackerModel trackerModel)
        {
            var storedProcedure = "dbo.LoginTracker_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("Username", trackerModel.Username);
            p.Add("LoginTime", trackerModel.LoginTime);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }
    }
}
