using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CorsAnywhere.Interfaces;
using CorsAnywhere.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CorsAnywhere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MakeAPICallController : ControllerBase
    {
        private readonly IMakeAPICallService _makeAPICallService;
        private readonly ILogger<MakeAPICallController> _logger;

        public MakeAPICallController(ILogger<MakeAPICallController> logger, IMakeAPICallService makeAPICallService)
        {
            _logger = logger;
            _makeAPICallService = makeAPICallService;
        }

        [HttpGet]
        public Task<ResponseWrapper> Get([FromBody] PostObject postData)
        {
            return _makeAPICallService.MakeGetAPICall(postData);
        }

        [HttpPost]
        public Task<ResponseWrapper> Post([FromBody] PostObject postData)
        {
            return _makeAPICallService.MakePostAPICall(postData);
        }
    }
}
