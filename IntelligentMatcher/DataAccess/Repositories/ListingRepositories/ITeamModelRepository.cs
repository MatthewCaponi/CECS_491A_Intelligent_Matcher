using Models.DALListingModels;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ListingRepositories
{
    public interface ITeamModelRepository
    {
        Task<int> CreateListing(DALTeamModel dalTeamModel);
        Task<int> UpdateListing(DALTeamModel dalTeamModel);
        Task<int> DeleteTeamListing(int id);
    }
}