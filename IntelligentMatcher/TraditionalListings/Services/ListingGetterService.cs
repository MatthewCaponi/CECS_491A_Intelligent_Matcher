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
    public class ListingGetterService
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


        public async Task<BusinessListingModel> GetListing(int id )
        {
            var listing = await _listingRepository.GetListing(id);
            var businesslistingmodel= ModelConverterService.ConvertTo(listing, new BusinessListingModel());
            return businesslistingmodel;
        }

        public async Task<List<BusinessListingModel>> GetAllListing()
        {
            var userAccounts = await _listingRepository.GetAllListings();
            List<BusinessListingModel> businessListingModel = new List<BusinessListingModel>();
            foreach (var userAccountModel in userAccounts)
            {
                var webUserAccountModel = ModelConverterService.ConvertTo(userAccountModel, new BusinessListingModel());
                businessListingModel.Add(webUserAccountModel);
            }

            return businessListingModel;



            
        }
       


    }
}
