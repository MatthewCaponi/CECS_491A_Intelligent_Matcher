using Models.DALListingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ITraditionalListingSearchRepository
    {
        Task<IEnumerable<DALListingModel>> GetAllListings();
        Task<IEnumerable<DALCollaborationModel>> GetAllCollaborationListings();
        Task<IEnumerable<DALRelationshipModel>> GetAllRelationshipListings();
        Task<IEnumerable<DALRelationshipModel>> GetAllTeamListings();




        // gold plating??? not in the brd stuff 

        Task<IEnumerable<DALListingModel>> SortListingbyCity();
        Task<IEnumerable<DALListingModel>> SortListingbyState();
        

    }
}
