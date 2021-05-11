using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserAnalysisManager;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAnalysisController : Controller
    {

        private readonly IUserAnalysisService _userAnalysisService;

        public UserAnalysisController(IUserAnalysisService userAnalysisService)
        {
            _userAnalysisService = userAnalysisService;
        }
        [HttpPost]
        public async Task<ActionResult<int>> GetRegistrationCount()
        {
            try
            {
                return Ok(await _userAnalysisService.GetALLUserRegistrationCount());

            }
            catch
            {
                return StatusCode(404);

            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> GetRegistrationCount_ByDay(DateTimeOffset date)
        {
            try
            {
                return Ok(await _userAnalysisService.GetALLUserRegistrationCount_Day(date));

            }
            catch
            {
                return StatusCode(404);

            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> GetRegistrationCount_ByMonth(int month, int year)
        {
            try
            {
                return Ok(await _userAnalysisService.GetALLUserRegistrationCount_Month(month,year));

            }
            catch
            {
                return StatusCode(404);

            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> GetRegistrationCount_ByYear(int year)
        {
            try
            {
                return Ok(await _userAnalysisService.GetALLUserRegistrationCount_Year(year));

            }
            catch
            {
                return StatusCode(404);

            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> GetRegistrationCount_Today()
        {
            try
            {
                return Ok(await _userAnalysisService.GetAllUsersRegisteredToday());

            }
            catch
            {
                return StatusCode(404);

            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> GetNumTotal_SuspendedAccount()
        {
            try
            {
                return Ok(await _userAnalysisService.GetNumOfSuspendedUsers());

            }
            catch
            {
                return StatusCode(404);

            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> GetNumTotal_BannedAccount()
        {
            try
            {
                return Ok(await _userAnalysisService.GetNumOfBannedUsers());

            }
            catch
            {
                return StatusCode(404);

            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> GetNumTotal_ShadowBannedAccount()
        {
            try
            {
                return Ok(await _userAnalysisService.GetNumOfShadowBannedUsers());

            }
            catch
            {
                return StatusCode(404);

            }
        }

    }
}
