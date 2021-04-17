using Models.DALListingModels;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public interface ITeamModelRepository
    {
        Task<int> CreateListing(DALTeamModel dalTeamModel);
        Task<int> DeleteListing(int id);
        Task<int> UpdateListing(DALTeamModel dalTeamModel);
    }
}