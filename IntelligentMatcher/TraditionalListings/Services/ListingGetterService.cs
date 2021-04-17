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

        private ITraditionalListingSearchRepository _traditionalListingSearchRepository;

        public ListingGetterService(ITraditionalListingSearchRepository traditionalListingSearchRepository)
        {
            _traditionalListingSearchRepository = traditionalListingSearchRepository;

        }



        public async Task<List<BusinessListingModel>> GetAllListing()
        {
            var dalListing = await _traditionalListingSearchRepository.GetAllListings();
            List<BusinessListingModel> businessListings = new List<BusinessListingModel>();
            foreach (var dalListingModel in dalListing)
            {
                var businessListing = ModelConverterService.ConvertTo(dalListingModel, new BusinessListingModel());
                businessListings.Add(businessListing);
            }

            return businessListings;

        }

        public async Task<List<BusinessCollaborationModel>> GetAllCollaborationListing()
        {
            var dalListing = await _traditionalListingSearchRepository.GetAllCollaborationListings();
            List<BusinessCollaborationModel> businessListings = new List<BusinessCollaborationModel>();
            foreach (var dalListingModel in dalListing)
            {
                var businessListing = ModelConverterService.ConvertTo(dalListingModel, new BusinessCollaborationModel());
                businessListings.Add(businessListing);
            }

            return businessListings;
        }

        public async Task<List<BusinessRelationshipModel>> GetAllRelationshipListing()
        {
            var dalListing = await _traditionalListingSearchRepository.GetAllRelationshipListings();
            List<BusinessRelationshipModel> businessListings = new List<BusinessRelationshipModel>();
            foreach (var dalListingModel in dalListing)
            {
                var businessListing = ModelConverterService.ConvertTo(dalListingModel, new BusinessRelationshipModel());
                businessListings.Add(businessListing);
            }

            return businessListings;
        }

        public async Task<List<BusinessTeamModel>> GetAllTeamModelListing()
        {
            var dalListing = await _traditionalListingSearchRepository.GetAllTeamListings();
            List<BusinessTeamModel> businessListings = new List<BusinessTeamModel>();
            foreach (var dalListingModel in dalListing)
            {
                var businessListing = ModelConverterService.ConvertTo(dalListingModel, new BusinessTeamModel());
                businessListings.Add(businessListing);
            }

            return businessListings;
        }





    }
}
