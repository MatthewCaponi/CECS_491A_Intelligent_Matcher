﻿using Models;
using System;
using Models.DALListingModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace DataAccess.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public ListingRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<int> CreateListing(DALListingModel dalListingModel)
        {
            string storedProcedure = "dbo.CreateListingParent";

            DynamicParameters p = new DynamicParameters();
            p.Add("Title", dalListingModel.Title);
            p.Add("Details", dalListingModel.Details);
            p.Add("City", dalListingModel.City);
            p.Add("State", dalListingModel.State);
            p.Add("NumberOfParticipants", dalListingModel.NumberOfParticipants);
            p.Add("InPersonOrRemote", dalListingModel.InPersonOrRemote);
            p.Add("UserAccountId", dalListingModel.UserAccountId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");



        }

        public async Task<int> DeleteListing(int id)// change to DALListingModel dalListingModel 
        {
            string storedProcedure = "dbo.DeleteListing";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
       
        public async Task<int> UpdateListing(DALListingModel dalListingModel)
        {
        
            var query = "dbo.EditParentListing";


            return await _dataGateway.Execute(query,
                                          new
                                          {
                                              Title = dalListingModel.Title,
                                              Details = dalListingModel.Details,
                                              City = dalListingModel.City,
                                              State= dalListingModel.State,
                                              NumberOfParticipants = dalListingModel.NumberOfParticipants,
                                              InPersonOrRemote = dalListingModel.InPersonOrRemote
                                          },
                                          _connectionString.SqlConnectionString) ; 
            ;

           
        }

    }
        
}

