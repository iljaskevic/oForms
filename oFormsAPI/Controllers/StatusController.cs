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
using oFormsAPI.Models;
using Microsoft.Extensions.Options;

namespace oFormsAPI.Controllers
{
    [Route("status")]
    [EnableCors("AllowAllOrigins")]
    public class StatusController : Controller
    {
        private readonly ILogger _logger;
        private readonly string _location;

        public StatusController(ILogger<HealthController> logger, IOptions<FormsConfiguration> _configuration)
        {
            _logger = logger;
            _location = _configuration.Value.Location;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        
        [HttpGet("location")]
        public IActionResult GetLocation()
        {
            return Content(_location);
        }

        [HttpGet("health")]
        public IActionResult GetHealth()
        {
            return Ok();
        }
    }
}
