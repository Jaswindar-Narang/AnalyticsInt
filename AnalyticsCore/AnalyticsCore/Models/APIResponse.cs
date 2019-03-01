using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsCore.Models
{
    public class APIResponse
    {
        bool IsSuccess = false;
        string Message;
        object ResponseData;


        public APIResponse(bool status, string message, object data)
        {
            IsSuccess = status;
            Message = message;
            ResponseData = data;
        }
    }
}
