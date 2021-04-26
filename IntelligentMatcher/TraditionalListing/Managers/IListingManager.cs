using System;
using BusinessModels;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Models;
using BusinessModels.ListingModels;

namespace TraditionalListings.Managers
{
    public interface IListingsManager

    {
        Task<Tuple<bool, ResultModel<int>>> DeleteListing(int Id);
        Task<Result<int>> CreateListing(WebUserProfileModel webUserProfileModel, BusinessListingModel businessListingModels);
        Task<Tuple<bool, ErrorMessage>> UpdateParentListing(BusinessListingModel businessListingModel);
        Task<Tuple<bool, ErrorMessage>> UpdateCollaborationListing(BusinessCollaborationModel businessCollaborationModel);
        Task<Tuple<bool, ErrorMessage>> UpdateRelationshipListing(BusinessRelationshipModel businessRelationshipModel);
        Task<Tuple<bool, ErrorMessage>> UpdateTeamListing(BusinessTeamModel businessTeamModel);




    }
}
