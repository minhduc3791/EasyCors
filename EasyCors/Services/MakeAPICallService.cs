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
using System.Web;

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
                SetHeader(postData.HeadersList);
                var builder = new UriBuilder(postData.RequestUrl);
                builder.Port = -1;
                string url = builder.ToString();
                var response = await _client.PostAsync(url, GetPostJson(postData.JsonData));

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
            ResponseWrapper responseWrapper = new ResponseWrapper();
            responseWrapper.Code = -1;

            try
            {
                SetHeader(postData.HeadersList);
                var builder = new UriBuilder(postData.RequestUrl);
                builder.Port = -1;
                var query = HttpUtility.ParseQueryString(builder.Query);
                if (postData.JsonData != null) {
                    var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(postData.JsonData);
                    foreach (KeyValuePair<string, string> entry in headers)
                    {
                        query[entry.Key] = entry.Value;
                    }
                    builder.Query = query.ToString();
                }
                string url = builder.ToString();
                var response = await _client.GetAsync(url);
                responseWrapper.Code = 1;
                responseWrapper.Data = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                responseWrapper.Data = ex.Message;
            }

            return responseWrapper;
        }

        private void SetHeader(string jsonString)
        {
            if (jsonString != null && jsonString.Length > 0)
            {
                var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    _client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                }
            }
        }
    }
}
