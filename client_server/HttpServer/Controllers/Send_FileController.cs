using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HttpServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Send_FileController : ControllerBase
    {
        // GET api/send_file
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post(string client_id)
        {
            using (var file = System.IO.File.Create(GetFileName()))
            {
                HttpContext.Request.Body.CopyTo(file);
            }
            return Ok();
        }

        private string GetFileName() => $"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.hex";
    }
}
