using Models.DALListingModels;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public interface ICollaborationRepository
    {
        Task<int> CreateListing(DALListingModel dalCollaborationModel);
        Task<DALCollaborationModel> GetListing(int id);
    }
}