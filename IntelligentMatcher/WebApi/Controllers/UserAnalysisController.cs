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
    }
}
