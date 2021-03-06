using Models.DALListingModels;
using System.Threading.Tasks;

namespace TraditionalListings.Services
{
    public interface IRelationshipRepository
    {
        Task<int> CreateListing(DALRelationshipModel dalRelationshipModel);
        Task<DALRelationshipModel> GetListing(int id);
        Task<int> UpdateListing(DALRelationshipModel dalRelationshipModell);
    }
}