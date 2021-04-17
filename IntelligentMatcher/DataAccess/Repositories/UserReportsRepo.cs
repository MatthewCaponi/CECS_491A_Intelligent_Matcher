using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;

namespace DataAccess.Repositories
{
    public class UserReportsRepo : IUserReportsRepo
    {

        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserReportsRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }


        public async Task<int> DeleteReportById(int id)
        {
            var storedProcedure = "dbo.UserReports_Delete";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<UserReportsModel>> GetAllReports()
        {
            string storedProcedure = "dbo.UserReports_GetAll";

            return await _dataGateway.LoadData<UserReportsModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> CreateReport(UserReportsModel model)
        {
            var storedProcedure = "dbo.UserReports_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("Report", model.Report);
            p.Add("ReportingId", model.ReportingId);
            p.Add("Date", model.Date);
            p.Add("ReportedId", model.ReportedId);


            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

    }
}
