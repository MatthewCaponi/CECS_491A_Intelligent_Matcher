using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public string ExtractHeader(HttpContext context, string headerName)
        {
            var request = context.Request;
            var header = request.Headers[headerName].ToString();
            return header;
        }

        public string [] ExtractHeader(HttpContext context, string headerName, char delimiter)
        {
            var header = ExtractHeader(context, headerName);
            var subHeaders = header.Split(' ');

            return subHeaders;
        }

        public string ExtractHeader(HttpContext context, string headerName, char delimiter, int index)
        {
            var subHeader = ExtractHeader(context, headerName, delimiter)[index];

            return subHeader;
        }
    }
}

