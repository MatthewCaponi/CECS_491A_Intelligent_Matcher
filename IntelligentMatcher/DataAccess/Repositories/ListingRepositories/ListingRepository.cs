using Models;
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
            var query = "insert into [Listing]([Title],[Details],[City],[State],[NumberOfParticipants], " +
                       "[InPersonOrRemote] , [UserAccountID]) values (@Title , @Details, @City, @State, " +
                       "@NumberOfParticipants, @InPersonOrRemote, @UserAccountID); " + "set @Id= SCOPE_IDENTITY();";

            DynamicParameters p = new DynamicParameters();
            p.Add("Title", dalListingModel.Title);
            p.Add("Details", dalListingModel.Details);
            p.Add("City", dalListingModel.City);
            p.Add("State", dalListingModel.State);
            p.Add("NumberOfParticipants", dalListingModel.NumberOfParticipants);
            p.Add("InPersonOrRemote", dalListingModel.InPersonOrRemote);
            p.Add("UserAccountId", dalListingModel.UserAccountId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.SaveData(query, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");



        }

        public async Task<int> DeleteListing(int id)// change to DALListingModel dalListingModel 
        {
            var query = "delete from [Listing] where Id = @Id";

            return await _dataGateway.SaveData(query,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }
        // combine into one update.. UpdateListing(DALListingModel dalListingModel)... multiple queries.
        // UpdateCollaborationListing(CollaborationModel collaborationmodel) ...multiple queries
        // ditto for the rest. 
        public async Task<int> UpdateListing(DALListingModel dalListingModel)
        {
        
            var query = "update Listing set Title = @Title , Details = @Details  where Id= @Id";


            return await _dataGateway.SaveData(query,
                                          new
                                          {
                                              Title = dalListingModel.Title,
                                              Details = dalListingModel.Details
                                          },
                                          _connectionString.SqlConnectionString) ; ;
            ;

           
        }

        public async Task<DALListingModel> GetListing(int id) //change to DALListingModel dalListingModel 
        {
            var query = "Select * from Listing where Id = @Id";

            var row = await _dataGateway.LoadData<DALListingModel, dynamic>(query,
                new
                {
                    ID=id
                },
                _connectionString.SqlConnectionString);

            return row.FirstOrDefault();
        }


        public async Task<List<DALListingModel>> GetAllListing() //change to DALListingModel dalListingModel 
        {
            var query = "Select * from Listing ";

            var row = await _dataGateway.LoadData<DALListingModel, dynamic>(query,
                new
                {
                   
                },
                _connectionString.SqlConnectionString);

            return row;
        }

    }
        
}

