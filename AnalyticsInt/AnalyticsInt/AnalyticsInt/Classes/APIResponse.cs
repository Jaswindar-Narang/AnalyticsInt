using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AnalyticsInt.Classes
{
    

    public class APIResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }
        public int? DataSize { get; set; }

    //public APIResponse(HttpStatusCode statusCode, string errorMessage, T result, int? dataSize)
    //    {
    //        StatusCode = statusCode;
    //        ErrorMessage = errorMessage;
    //        Result = result;
    //        DataSize = dataSize;
    //    }
    }
}
