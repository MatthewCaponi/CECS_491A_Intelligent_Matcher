using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.PageVisitTrackerRepositories
{
    public class PageVisitTrackerRepo : IPageVisitTrackerRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public PageVisitTrackerRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DALPageVisitTrackerModel>> GetAllAccounts()
        {
            string storedProcedure = "dbo.PageVisitTracker_GetALL";

            return await _dataGateway.LoadData<DALPageVisitTrackerModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> DeletebyId(int id)
        {
            var storedProcedure = "dbo.PageVisitTracker_DeletebyId";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> CreatePageVisitTracker(DALPageVisitTrackerModel trackerModel)
        {
            var storedProcedure = "dbo.PageVisitTracker_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("PageVisitedName", trackerModel.PageVisitedName);
            p.Add("PageVisitTime", trackerModel.PageVisitTime);
            p.Add("UserId", trackerModel.UserId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }
    }
}
