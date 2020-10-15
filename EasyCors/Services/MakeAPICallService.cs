using CorsAnywhere.Interfaces;
using CorsAnywhere.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CorsAnywhere.Services
{
    public class MakeAPICallService : IMakeAPICallService
    {
        private HttpClient _client;

        public MakeAPICallService() 
        {
            _client = new HttpClient();
        }
        private StringContent GetPostData(string jsonString, string dataType)
        {
            return new StringContent(jsonString, Encoding.UTF8, dataType);
        }

        private StringContent GetPostJson(string data)
        {
            return GetPostData(data, "application/json");
        }

        public async Task<ResponseWrapper> MakePostAPICall(PostObject postData)
        {
            ResponseWrapper responseWrapper = new ResponseWrapper();
            responseWrapper.Code = -1;

            try
            {
                if (postData.HeadersList != null)
                {
                    var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(postData.HeadersList);
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        _client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }
                }

                var response = await _client.PostAsync(postData.RequestUrl, GetPostJson(postData.JsonData));
                responseWrapper.Code = 1;
                responseWrapper.Data = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                responseWrapper.Data = ex.Message;
            }

            return responseWrapper;
        }

        public async Task<ResponseWrapper> MakeGetAPICall(PostObject postData)
        {
            return await MakeAPICall(postData, "GET");
        }
        public async Task<ResponseWrapper> MakeAPICall(PostObject postData, string method)
        {
            /*
             * url: https://api3.yolearn.vn/yolearner/api/login/,
                header: {
                    "content-type": application/json
                },
                body: {
                   username: "0346253997",
                   password: "12345678"
                }
             */
            ResponseWrapper responseWrapper = new ResponseWrapper();
            responseWrapper.Code = -1;

            try
            {
                if (postData.HeadersList != null)
                {
                    var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(postData.HeadersList);
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        _client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                    }
                }

                var response = await _client.GetAsync(postData.RequestUrl);
                responseWrapper.Code = 1;
                responseWrapper.Data = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex) 
            {
                responseWrapper.Data = ex.Message;
            }

            return responseWrapper;
        }
    }
}
