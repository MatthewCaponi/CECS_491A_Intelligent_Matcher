using Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class TokenObj
    {
        public string Authorization { get; set; }
    }
    [ApiController]
    public class TokenEndpointController : ControllerBase
    {
        private readonly ILogService _logService;

        public TokenEndpointController(ILogService logService)
        {
            _logService = logService;
        }
        [Route("[controller]/[action]")]
        [HttpGet]
        public ActionResult<string> ReceiveToken()
        {
            var context = HttpContext.Request;
            _logService.Log(context.Headers["Authorization"].ToString(), LogTarget.All, LogLevel.info, this.ToString(), "API_Dev_Logs");
            return context.Headers["Authorization"].ToString();
        }
    }
}
