using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Models;
using System.Collections.Generic;
using FriendList;
using DataAccess;
using PublicUserProfile;
using DataAccess.Repositories;

namespace IntelligentMatcherUI.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {

     

        /*
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
        private readonly IPublicUserProfileManager _publicUserProfileManager;

        public UserProfileController(IPublicUserProfileManager publicUserProfileManager)
        {
            _publicUserProfileManager = publicUserProfileManager;
        }


        [HttpPost]
        public async Task<PublicUserProfileModel> GetUserProfile([FromBody] int userId)
        {

            return await _publicUserProfileManager.GetUserProfile(userId);

        }


    }
}
