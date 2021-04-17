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
    public interface IPublicUserProfileRepo
    {

        Task<IEnumerable<PublicUserProfileModel>> GetAllPublicProfiles();

        Task<PublicUserProfileModel> GetPublicProfilebyUserId(int userId);

        Task<int> DeletePublicProfileById(int id);

        Task<int> CreatePublicProfile(PublicUserProfileModel model);

        Task<int> UpdateDescription(int userId, string description);

        Task<int> UpdateIntrests(int userId, string intrests);

        Task<int> UpdateHobbies(int userId, string hobbies);

        Task<int> UpdateJobs(int userId, string jobs);

        Task<int> UpdateGoals(int userId, string goals);

        Task<int> UpdateAge(int userId, int age);

        Task<int> UpdateGender(int userId, string gender);

        Task<int> UpdateEthnicity(int userId, string ethnicity);

        Task<int> UpdateSexualOrientation(int userId, string sexualOrientation);

        Task<int> UpdateHeight(int userId, string height);

        Task<int> UpdateVisibility(int userId, string visibility);

        Task<int> UpdateStatus(int userId, string status);

        Task<int> UpdatePhoto(int userId, string photo);
    }
}
