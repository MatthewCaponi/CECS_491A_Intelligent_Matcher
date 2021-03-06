using Models.DALListingModels;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public interface ICollaborationRepository
    {
        Task<int> CreateListing(DALCollaborationModel dalCollaborationModel);
        Task<DALCollaborationModel> GetListing(int id);
        Task<int> UpdateListing(DALListingModel dalListingModel);
    }
}