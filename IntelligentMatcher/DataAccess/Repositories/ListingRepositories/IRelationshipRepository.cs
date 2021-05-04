using Models.DALListingModels;
using System.Threading.Tasks;

namespace DataAccess.Repositories.ListingRepositories
{
    public interface IRelationshipRepository
    {
        Task<int> CreateListing(DALRelationshipModel dalRelationshipModel);
        Task<int> UpdateListing(DALRelationshipModel dalRelationshipModell);
    }
}