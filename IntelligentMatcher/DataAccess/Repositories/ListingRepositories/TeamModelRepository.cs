using Dapper;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TraditionalListings.Services;

namespace DataAccess.Repositories.ListingRepositories
{
    public class TeamModelRepository : ITeamModelRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public TeamModelRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> CreateListing(DALTeamModel dalTeamModel)
        {

            var query = "insert into [TeamModel]([TeamType],[GameType],[Platform],[Experience],[ListingId])" +
                "values (@TeamType, @GameType,@Platform,@Experience,@ListingId); set @Id = SCOPE_IDENTITY();";

            DynamicParameters p = new DynamicParameters();
            p.Add("TeamType", dalTeamModel.TeamType);
            p.Add("GameType", dalTeamModel.GameType);
            p.Add("Platform", dalTeamModel.Platform);
            p.Add("Experience", dalTeamModel.ListingId);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(query, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");



        }

        public Task<int> DeleteListing(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateListing(DALTeamModel dalTeamModel)
        {
            var storedProcedure = "dbo.EditTeamAttributes";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             DalTeamModel_TeamType = dalTeamModel.TeamType,
                                             DalTeamModel_GameType=dalTeamModel.GameType,
                                             DalTeamModel_Platform = dalTeamModel.Platform,
                                             DalTeamModel_Experience = dalTeamModel.Experience,

                                         },
                                         _connectionString.SqlConnectionString);
        }
    }

       
}

