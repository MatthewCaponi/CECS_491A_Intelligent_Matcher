using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Models;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
namespace DataAccess.Repositories
{
    public class PublicUserProfileRepo : IPublicUserProfileRepo
    {
        private readonly IDataGateway _dataGateway;
        private readonly IConnectionStringData _connectionString;
        public PublicUserProfileRepo(IDataGateway dataGateway, IConnectionStringData connectionString)
        {
            _dataGateway = dataGateway;
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<PublicUserProfileModel>> GetAllPublicProfiles()
        {
            string storedProcedure = "dbo.PublicUserProfile_Get_All";

            return await _dataGateway.LoadData<PublicUserProfileModel, dynamic>(storedProcedure,
                                                                          new { },
                                                                          _connectionString.SqlConnectionString);
        }

        public async Task<int> DeletePublicProfileById(int id)
        {
            var storedProcedure = "dbo.PublicUserProfile_Delete_ById";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             Id = id
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> CreatePublicProfile(PublicUserProfileModel model)
        {
            var storedProcedure = "dbo.PublicUserProfile_Create";

            DynamicParameters p = new DynamicParameters();

            p.Add("UserId", model.UserId);
            p.Add("Description", model.Description);
            p.Add("Intrests", model.Intrests);
            p.Add("Hobbies", model.Hobbies);
            p.Add("Jobs", model.Jobs);
            p.Add("Goals", model.Goals);
            p.Add("Age", model.Age);
            p.Add("Gender", model.Gender);
            p.Add("Ethnicity", model.Ethnicity);
            p.Add("SexualOrientation", model.SexualOrientation);
            p.Add("Height", model.Height);
            p.Add("Visibility", model.Visibility);
            p.Add("Status", model.Status);
            p.Add("Photo", model.Photo);

            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _dataGateway.Execute(storedProcedure, p, _connectionString.SqlConnectionString);

            return p.Get<int>("Id");
        }


        public async Task<PublicUserProfileModel> GetPublicProfilebyUserId(int userId)
        {
            string storedProcedure = "dbo.PublicUserProfile_GetByUserId";

            var row = await _dataGateway.LoadData<PublicUserProfileModel, dynamic>(storedProcedure,
                                                                          new
                                                                          {
                                                                              UserId = userId
                                                                          },
                                                                          _connectionString.SqlConnectionString);
            return row.FirstOrDefault();

        }


        public async Task<int> UpdateDescription(int userId, string description)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateDescription";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Description = description
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateIntrests(int userId, string intrests)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateIntrests";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Intrests = intrests
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateHobbies(int userId, string hobbies)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateHobbies";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Hobbies = hobbies
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateJobs(int userId, string jobs)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateJobs";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Jobs = jobs
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateGoals(int userId, string goals)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateGoals";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Goals = goals
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateAge(int userId, int age)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateAge";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Age = age
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateGender(int userId, string gender)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateGender";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Gender = gender
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateEthnicity(int userId, string ethnicity)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateEthnicity";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Ethnicity = ethnicity
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateSexualOrientation(int userId, string sexualOrientation)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateSexualOrientation";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             SexualOrientation = sexualOrientation
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateHeight(int userId, string height)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateHeight";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Height = height
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateVisibility(int userId, string visibility)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateVisibility";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Visibility = visibility
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdateStatus(int userId, string status)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdateStatus";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Status = status
                                         },
                                         _connectionString.SqlConnectionString);
        }

        public async Task<int> UpdatePhoto(int userId, string photo)
        {
            var storedProcedure = "dbo.PublicUserProfile_UpdatePhoto";

            return await _dataGateway.Execute(storedProcedure,
                                         new
                                         {
                                             UserId = userId,
                                             Photo = photo
                                         },
                                         _connectionString.SqlConnectionString);
        }

    }
}
