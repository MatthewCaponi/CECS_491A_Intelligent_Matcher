using ControllerModels;
using Help;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IHelpManager _helpManager;

        public HelpController(IHelpManager helpManager)
        {
            _helpManager = helpManager;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SendHelpEmail([FromBody] HelpModel helpModel)
        {
            if (helpModel == null)
            {
                return StatusCode(400);
            }

            if (helpModel.Subject == null || helpModel.Message == null)
            {
                return StatusCode(400);
            }

            return Ok(await _helpManager.SendHelpEmail(helpModel.Subject, helpModel.Message));
        }
    }
}
