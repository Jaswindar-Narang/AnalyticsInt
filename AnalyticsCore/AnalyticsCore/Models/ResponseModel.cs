using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AnalyticsCore.Models
{
    public class ResponseModel
    {
        
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public JObject Result { get; set; }
        public int? DataSize { get; set; }
    }
}
