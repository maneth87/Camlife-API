using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class ResponseExecuteError
    {
        public string status { get { return "400"; } }
        public string code { get; set; }

        public string message { get; set; }
    }
}