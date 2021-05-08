using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.SearchTrackerRepositories
{
    public class SearchTrackerRepo : ISearchTrackerRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public SearchTrackerRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DALSearchTrackerModel>> GetAllSearches()
        {
            string storedProcedure = "dbo.SearchTracker_GetAll";

            return await _dataGateway.LoadData<DALSearchTrackerModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }


        public async Task<int> DeletebyId(int id)
        {
            var storedProcedure = "dbo.SearchTracker_DeletebyId";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> CreatePageVisitTracker(DALSearchTrackerModel trackerModel)
        {
            var storedProcedure = "dbo.SearchTracker_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("Search", trackerModel.Search);
            p.Add("SearchTime", trackerModel.SearchTime);
            p.Add("UserId", trackerModel.UserId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }
    }
}
