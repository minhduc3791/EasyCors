using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CorsAnywhere.Interfaces;
using CorsAnywhere.Models;
using EasyCors.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CorsAnywhere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<MakeAPICallController> _logger;

        public UserController(ILogger<MakeAPICallController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Create new EasyCorsUser
        /// </summary>
        /// <param name="ecUser"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost]
        public async Task<ResponseWrapper> RegisterECUser([FromBody] ECUser ecUser)
        {
            ResponseWrapper response = new ResponseWrapper();
            return response;
        }
    }
}
