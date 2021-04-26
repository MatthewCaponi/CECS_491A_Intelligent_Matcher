using Models.DALListingModels;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ListingRepositories
{
    public interface IDatingRepository
    {
        Task<int> CreateListing(DALDatingModel dalDatingModel);
        Task<DALDatingModel> GetListing(int id);
        Task<int> UpdateListing(DALDatingModel dalDatingModel);
    }
}