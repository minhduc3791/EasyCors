using CorsAnywhere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorsAnywhere.Interfaces
{
    public interface IMakeAPICallService
    {
        Task<ResponseWrapper> MakePostAPICall(PostObject postData);
        Task<ResponseWrapper> MakeGetAPICall(PostObject postData);
    }
}
