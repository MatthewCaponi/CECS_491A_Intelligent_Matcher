using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;
namespace DataAccess.Repositories
{
    public class FriendBlockListRepo : IFriendBlockListRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public FriendBlockListRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }


        public async Task<int> DeleteFriendBlockbyUserIds(int userId1, int userId2)
        {
            var storedProcedure = "dbo.FriendBlockList_Delete";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId1 = userId1,
                                             UserId2 = userId2
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteFriendBlockbyId(int id)
        {
            var storedProcedure = "dbo.FriendBlockList_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriendBlocks(int userId)
        {
            string storedProcedure = "dbo.FriendBlockList_GetAll_ByUserId";

            return await _dataGateway.LoadData<FriendsListJunctionModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              UserId = userId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> AddFriendBlock(FriendsListJunctionModel model)
        {
            var storedProcedure = "dbo.Add_FriendBlock";

            DynamicParameters p = new DynamicParameters();

            p.Add("User1Id", model.User1Id);
            p.Add("User2Id", model.User2Id);
            p.Add("Date", model.Date);


            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllFriendBlocks()
        {
            string storedProcedure = "dbo.FriendBlockList_GetAll";

            return await _dataGateway.LoadData<FriendsListJunctionModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }
        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllBlockingUser(int userId)
        {
            string storedProcedure = "dbo.FriendBlockList_GetAll_ByUserId";

            return await _dataGateway.LoadData<FriendsListJunctionModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              UserId = userId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

    }
}
