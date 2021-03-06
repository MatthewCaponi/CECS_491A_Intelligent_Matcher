using BusinessModels.ListingModels;
using DataAccess.Repositories;
using Models;
using Models.DALListingModels;
using Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace TraditionalListings.Services
{
    class ListingGetterService
    {

        private IListingRepository _listingRepository;
        private  ICollaborationRepository _collaborationRepository;
        private IDatingRepository _datingRepository;
        private IRelationshipRepository _relationshipRepository;
        private  ITeamModelRepository _teamModelRepository;

        public ListingGetterService(IListingRepository listingRepository, ICollaborationRepository collaborationRepository, IDatingRepository datingRepository, IRelationshipRepository relationshipRepository,
            ITeamModelRepository teamModelRepository)
        {
            _collaborationRepository = collaborationRepository;
            _datingRepository = datingRepository;
            _relationshipRepository = relationshipRepository;
            _teamModelRepository = teamModelRepository;
            _listingRepository = listingRepository;
        }

        
   




    }
}
