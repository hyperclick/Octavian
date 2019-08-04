using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        // GET api/send
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(string client_id, string data)
        {
            return new string[] { client_id, data };
        }

    }
}
