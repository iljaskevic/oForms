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

namespace oFormsAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [EnableCors("AllowAllOrigins")]
    public class FormController : Controller
    {
        private readonly ILogger _logger;
        IApiFormRepository _apiFormRepository;
        IMessageService _messageService;
        private readonly IMemoryCache _cache;

        public FormController(IApiFormRepository apiFormRepository, IMessageService messageService, IMemoryCache memoryCache, ILogger<FormController> logger)
        {
            _apiFormRepository = apiFormRepository;
            _messageService = messageService;
            _cache = memoryCache;
            _logger = logger;
        }
        /*
                // To be used once dynamic CORS is added as a feature
                [HttpOptions]
                public HttpResponseMessage Options()
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.OK);
                    StringValues hostHeader;
                    string origin = null;
                    if (Request.Headers.TryGetValue("Origin", out hostHeader))
                    {
                        origin = hostHeader.ElementAt<string>(0);
                        var uri = new Uri(origin);
                        origin = uri.Host;
                        _logger.LogDebug($"Found CORS domain: {origin}");
                    }
                    else
                    {
                        _logger.LogError($"No CORS domain");
                        origin = "oformsapi.azurewebsites.net";
                        //return Unauthorized();
                    }
                    string domain = _apiFormRepository.GetCORSDomain(origin).Result;
                    resp.Headers.Add("Access-Control-Allow-Origin", $"{origin}");
                    resp.Headers.Add("Access-Control-Allow-Methods", "GET,POST");

                    return resp;
                }
                */
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]JObject formData)
        {
            _logger.LogDebug($"Received: {formData}");

            if (formData == null)
            {
                _logger.LogError($"No data submitted");
                return BadRequest();
            }

            StringValues apiHeader;
            string token = null;
            if (Request.Headers.TryGetValue("api-key", out apiHeader))
            {
                token = apiHeader.ElementAt<string>(0);
                _logger.LogDebug($"Found 'api-key' header: {token}");
            }
            else
            {
                _logger.LogError($"No api-key header");
                return Unauthorized();
            }

            _logger.LogInformation("Submitted Token: " + token);

            FormApiMap formApiMap;
            // Look for cache key.
            if (!_cache.TryGetValue(token, out formApiMap))
            {
                _logger.LogInformation("API key (" + token + ") not found in cache. Calling FormRepository!");
                var startCache = DateTime.Now;
                _logger.LogInformation("Started API Key Middleware lookup: " + startCache.ToString());
                formApiMap = _apiFormRepository.GetFormApiMap(token).Result;
                var endCache = DateTime.Now;
                _logger.LogInformation("Finished API Key Middleware lookup: " + DateTime.Now.ToString() + " - (" + endCache.Subtract(startCache).TotalMilliseconds + "ms)");


                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                // Save data in cache.
                _cache.Set(token, formApiMap, cacheEntryOptions);
            }
            else
            {
                _logger.LogInformation("API key (" + token + ") found in cache!");
            }


            _logger.LogInformation("Sending data for Key: " + formData);
            _messageService.SendEmailAsync(formApiMap.EmailInfo, formData.ToString());

            return Ok(new { });
        }
    }
}
