using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class ResponseExecuteSuccess
    {
        public string status { get { return "200"; } }
        public string code { get { return "1000"; } }
        public string message { get; set; }

        //public MyResponse1 DETAIL { get; set; }
        public object detail { get; set; }
    }
}