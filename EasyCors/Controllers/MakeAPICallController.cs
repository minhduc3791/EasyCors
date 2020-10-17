using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CorsAnywhere.Interfaces;
using CorsAnywhere.Models;
using Microsoft.AspNetCore.Authorization;
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

        /*[HttpGet]
        public Task<ResponseWrapper> Get([FromBody] PostObject postData)
        {
            return _makeAPICallService.MakeGetAPICall(postData);
        }*/
        /// <summary>
        /// Take the request url and params, make the request and return the result of that request or error if occurs.
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPost]
        public async Task<ResponseWrapper> Post([FromBody] PostObject postData)
        {
            ResponseWrapper response = new ResponseWrapper();
            
            try
            {
                if (postData.Method.Equals("POST"))
                {

                    response = await _makeAPICallService.MakePostAPICall(postData);
                    return response;
                }
                else
                {
                    response = await _makeAPICallService.MakeGetAPICall(postData);
                    return response;
                }
            }
            catch 
            {
                response.Code = -1;
                response.Data = "Error, Invalid or missing parameter(s).";
                return response;
            }
        }
    }
}
