using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Models;
using System.Collections.Generic;
using FriendList;
using DataAccess;
using DataAccess.Repositories;

namespace IntelligentMatcherUI.Controllers
{

    [Route("[controller]/[action]")]
    [ApiController]
    public class FriendListController : ControllerBase
    {

        public class dualIdModel
        {
            public int UserId { get; set; }
            public int FriendId { get; set; }
        }

        public class idUsernameModel
        {
            public int UserId { get; set; }
            public string FriendUsername { get; set; }
        }

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
        private readonly IFriendListManager _friendListManager;
        private readonly IUserAccountRepository _userAccountRepository;

        public FriendListController(IFriendListManager friendListManager, IUserAccountRepository userAccountRepository)
        {
            _friendListManager = friendListManager;
            _userAccountRepository = userAccountRepository;
        }


        [HttpPost]
        public async Task<IEnumerable<FriendListModel>> GetAllFriends([FromBody] int userId)
        {

            return await _friendListManager.GetAllFriendAsync(userId);

        }

        [HttpPost]
        public async Task<IEnumerable<FriendListModel>> GetAllRequets([FromBody] int userId)
        {

            return await _friendListManager.GetAllRequestsAsync(userId);

        }

        [HttpPost]
        public async Task<IEnumerable<FriendListModel>> GetAllRequetsOutgoing([FromBody] int userId)
        {

            return await _friendListManager.GetAllRequestsOutgoingAsync(userId);

        }

        [HttpPost]
        public async Task<bool> RemoveFriend([FromBody] dualIdModel ids)
        {

             await _friendListManager.RemoveFriendAsync(ids.UserId, ids.FriendId);
            return true;
        }



        [HttpPost]
        public async Task<bool> BlockFriend([FromBody] dualIdModel ids)
        {

            await _friendListManager.BlockFriendAsync(ids.UserId, ids.FriendId);
            return true;
        }


        [HttpPost]
        public async Task<bool> ApproveFriend([FromBody] dualIdModel ids)
        {

            await _friendListManager.ConfirmFriendAsync(ids.UserId, ids.FriendId);
            return true;
        }

        [HttpPost]
        public async Task<bool> CancelFriendRequest([FromBody] dualIdModel ids)
        {

            await _friendListManager.CancelFriendRequestAsync(ids.UserId, ids.FriendId);
            return true;
        }

        

        [HttpPost]
        public async Task<IEnumerable<FriendListModel>> GetAllBlocking([FromBody] int userId)
        {

            return await _friendListManager.GetAllBlockingUserAsync(userId);

        }
        [HttpPost]
        public async Task<IEnumerable<FriendListModel>> GetAllBlocks([FromBody] int userId)
        {

            return await _friendListManager.GetAllBlocksAsync(userId);

        }

        [HttpPost]
        public async Task<bool> CreateFriendRequest([FromBody] idUsernameModel ids)
        {
            UserAccountModel model = await _userAccountRepository.GetAccountByUsername(ids.FriendUsername);
            int friendId = model.Id;
            await _friendListManager.RequestFriendAsync(friendId, ids.UserId);
            return true;
        }



    }
}
