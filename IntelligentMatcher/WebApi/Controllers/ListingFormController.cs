using BusinessModels.ListingModels;
using ControllerModels.ListingModel;
using ControllerModels.RegistrationModels;
using IntelligentMatcher.UserManagement;
using Listings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ListingFormController : ControllerBase
    {
        private readonly IListingManager _listingManager;
   
        public ListingFormController(IListingManager listingmanager)
        {
            _listingManager = listingmanager;
            
        }

        [HttpPost]
        public async Task<ActionResult<bool>> FilloutForm([FromBody] ParentListingModel parentListingModel)
        {
           
                var listingForm = new BusinessListingModel();
                listingForm.Title = parentListingModel.Title;
                listingForm.Details = parentListingModel.Details;
                listingForm.City = parentListingModel.City;
                listingForm.State = parentListingModel.State;
                listingForm.InPersonOrRemote = parentListingModel.InPersonOrRemote;
                listingForm.NumberOfParticipants = parentListingModel.NumberOfParticipants;
                listingForm.UserAccountId = parentListingModel.UserAccountId;
               

            try
            {
                var fillOutResult = await _listingManager.CreateListing(listingForm);
                return true;
            }
            catch
            {
                return false ;
            }
                

             
               
            
           
           
        }
    }
}
