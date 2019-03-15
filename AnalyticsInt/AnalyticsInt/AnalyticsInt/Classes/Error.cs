using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyticsInt.Classes
{
    public class Error
    {
        public int httpStatusCode { get; set; }
        public string errorCode { get; set; }
        public string errorId { get; set; }
        public string errorMessage { get; set; }
    }
}
