using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Models;
namespace DataAccess.Repositories
{
    public class FriendListRepo : IFriendListRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public FriendListRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }


        public async Task<int> DeleteFriendListbyUserIds(int userId1, int userId2)
        {
            var storedProcedure = "dbo.FriendsList_Delete";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId1 = userId1,
                                             UserId2 = userId2
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> DeleteFriendListbyId(int id)
        {
            var storedProcedure = "dbo.FriendsList_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllUserFriends(int userId)
        {
            string storedProcedure = "dbo.FriendsList_GetAll_ByUserId";

            return await _dataGateway.LoadData<FriendsListJunctionModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              UserId = userId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> AddFriend(FriendsListJunctionModel model)
        {
            var storedProcedure = "dbo.Add_Friend";

            DynamicParameters p = new DynamicParameters();

            p.Add("User1Id", model.User1Id);
            p.Add("User2Id", model.User2Id);
            p.Add("Date", model.Date);


            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }

        public async Task<IEnumerable<FriendsListJunctionModel>> GetAllFriends ()
        {
            string storedProcedure = "dbo.FriendsList_GetAll";

            return await _dataGateway.LoadData<FriendsListJunctionModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }


    }
}
