using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AnalyticsInt.Classes
{
    

    public class APIResponse<T>
    {
        public HttpStatusCode statusCode { get; set; }
        public string errorMessage { get; set; }
        public T result { get; set; }
        public int? dataSize { get; set; }

    //public APIResponse(HttpStatusCode statusCode, string errorMessage, T result, int? dataSize)
    //    {
    //        StatusCode = statusCode;
    //        ErrorMessage = errorMessage;
    //        Result = result;
    //        DataSize = dataSize;
    //    }
    }
}
