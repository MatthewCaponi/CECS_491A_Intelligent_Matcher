using Dapper;
using Models;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;
using Exceptions;
using DataAccessLayer.CrossCuttingConcerns;

namespace DataAccess.Repositories
{
    public class UserProfileRepository : DataAccessBase, IUserProfileRepository
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;

        public UserProfileRepository(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<Result<IEnumerable<UserProfileModel>>> GetAllUserProfiles()
        {
            string storedProcedure = "dbo.UserProfile_Get_All";
            try
            {
                var profiles = await _dataGateway.LoadData<UserProfileModel, dynamic>(storedProcedure,
                                                                              new { },
                                                                              _connectionString.SqlConnectionString);

                return Result<IEnumerable<UserProfileModel>>.Success(profiles);
            }
            catch (SqlCustomException e)
            {
                return Result<IEnumerable<UserProfileModel>>.Failure(e.ToString());
            }
        }

        public async Task<Result<UserProfileModel>> GetUserProfileById(int id)
        {
            string storedProcedure = "dbo.UserProfile_Get_ById";

            try
            {
                var row = await _dataGateway.LoadData<UserProfileModel, dynamic>(storedProcedure,
                    new
                    {
                        Id = id
                    },
                    _connectionString.SqlConnectionString);

                return Result<UserProfileModel>.Success(row.FirstOrDefault());
            }
            catch (SqlCustomException e)
            {
                return Result<UserProfileModel>.Failure(e.ToString());
            }
        }

        public async Task<Result<UserProfileModel>> GetUserProfileByAccountId(int accountId)
        {
            string storedProcedure = "dbo.UserProfile_Get_ByAccountId";

            try
            {
                var row = await _dataGateway.LoadData<UserProfileModel, dynamic>(storedProcedure,
                    new
                    {
                        UserAccountId = accountId
                    },
                    _connectionString.SqlConnectionString);

                return Result<UserProfileModel>.Success(row.FirstOrDefault());
            }
            catch (SqlCustomException e)
            {
                return Result<UserProfileModel>.Failure(e.ToString());
            }
        }

        public async Task<Result<int>> CreateUserProfile(UserProfileModel model)
        {
            if (!InvalidLength(model, 50))
            {
                return Result<int>.Failure(ErrorMessage.InvalidLength.ToString());
            }

            if (!IsNull(model))
            {
                return Result<int>.Failure(ErrorMessage.NullObject.ToString());
            }

            if (!ContainsNullOrEmptyParameter(model))
            {
                return Result<int>.Failure(ErrorMessage.NullOrEmptyParameter.ToString());
            }

            var storedProcedure = "dbo.UserProfile_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("FirstName", model.FirstName);
            p.Add("Surname", model.Surname);
            p.Add("DateOfBirth", model.DateOfBirth);
            p.Add("UserAccountId", model.UserAccountId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);
                return Result<int>.Success(p.Get<int>("Id"));
            }
            catch (SqlCustomException e)
            {
                return Result<int>.Failure(e.ToString());
            }
        }

        public async Task<Result<bool>> DeleteUserProfileByAccountId(int userAccountId)
        {
            var storedProcedure = "dbo.UserProfile_Delete_ById";

            try
            {
                await _dataGateway.Execute(storedProcedure,
                                             new
                                             {
                                                 UserAccountId = userAccountId
                                             },
                                             _connectionString.SqlConnectionString);

                return Result<bool>.Success(true);
            }
            catch (SqlCustomException e)
            {
                return Result<bool>.Failure(e.ToString());
            }
        }
    }
}
