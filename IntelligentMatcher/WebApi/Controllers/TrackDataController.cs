using UserAnalysisManager;
using BusinessModels;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Models;
using System.Collections.Generic;
using FriendList;
using DataAccess;
using DataAccess.Repositories;

namespace WebApi.Controllers
{
    public class TrackDataController
    {

        private readonly IUserAnalysisService _userAnalysisService;
      

        public TrackDataController(IUserAnalysisService userAnalysisService)
        {
            _userAnalysisService = userAnalysisService;
           
        }

        [HttpPost]
        public async Task<ActionResult<bool>> TrackLogin([FromBody] BusinessLoginTrackerModel model)
        {

            
            try
            {
                await _userAnalysisService.CreateLoginTracked(model); 
                return true;
            }
            catch
            {
                return false;
            }
           
        }
    }
}
