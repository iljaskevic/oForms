using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using oFormsAPI.Repositories;
using oFormsAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Primitives;
using oFormsAPI.Models.v1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;

namespace oFormsAPI.Controllers
{
    [Route("health")]
    [EnableCors("AllowAllOrigins")]
    public class HealthController : Controller
    {
        private readonly ILogger _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}
