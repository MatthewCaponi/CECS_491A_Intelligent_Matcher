using Dapper;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;


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
            string storedProcedure = "dbo.CreateTeamModelListing";

            DynamicParameters p = new DynamicParameters();
            p.Add("TeamType", dalTeamModel.TeamType);
            p.Add("GameType", dalTeamModel.GameType);
            p.Add("Platform", dalTeamModel.Platform);
            p.Add("Experience", dalTeamModel.ListingId);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");



        }

        public async Task<int> DeleteTeamListing(int id)
        {
            var storedProcedure = "dbo.DeleteTeam";
            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id

                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateListing(DALTeamModel dalTeamModel)
        {
            var storedProcedure = "dbo.EditTeamAttributes";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             TeamType = dalTeamModel.TeamType,
                                             GameType=dalTeamModel.GameType,
                                             Platform = dalTeamModel.Platform,
                                             Experience = dalTeamModel.Experience,

                                         },
                                         _connectionString.SqlConnectionString);
        }
    }

       
}

