using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HttpServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Send_FileController : ControllerBase
    {
        private readonly Config config;

        public Send_FileController(Config config)
        {
            this.config = config;
        }

        // GET api/send_file
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post(string client_id)
        {
            using (var file = System.IO.File.Create(Path.Combine(config.OutputFolder, GetFileName())))
            {
                HttpContext.Request.Body.CopyTo(file);
            }
            return Ok();
        }

        private string GetFileName() => $"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.hex";
    }
}
