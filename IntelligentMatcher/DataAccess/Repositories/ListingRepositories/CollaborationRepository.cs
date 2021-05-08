﻿using Dapper;
using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Repositories.ListingRepositories
{
    public class CollaborationRepository : ICollaborationRepository
    {

        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public CollaborationRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> CreateListing(DALCollaborationModel dalCollaborationModel)
        {

            string storedProcedure = "dbo.CreateCollaborationListing";

            DynamicParameters p = new DynamicParameters();
            p.Add("CollaborationType", dalCollaborationModel.CollaborationType);
            p.Add("InvolvementType", dalCollaborationModel.InvolvementType);
            p.Add("Experience", dalCollaborationModel.Experience);
            p.Add("ListingId", dalCollaborationModel.ListingId);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");



        }

        public async Task<int> DeleteCollabListing(int id)
        {
            var storedProcedure = "dbo.DeleteCollaboration";
            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id=id

                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateListing(DALCollaborationModel dALCollaborationModel)
        {
            var storedProcedure = "dbo.EditCollaborationAttributes";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             CollaborationType = dALCollaborationModel.CollaborationType,
                                             InvolvementType = dALCollaborationModel.InvolvementType,
                                             Experience = dALCollaborationModel.Experience

                                         },
                                         _connectionString.SqlConnectionString) ;
        }
    }

        
    
}
