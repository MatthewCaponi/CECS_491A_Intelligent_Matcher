using Models.DALListingModels;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ListingRepositories
{
    public interface ICollaborationRepository
    {
        Task<int> CreateListing(DALCollaborationModel dalCollaborationModel);
        Task<int> UpdateListing(DALCollaborationModel dALCollaborationModel);
    }
}