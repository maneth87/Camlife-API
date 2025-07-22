using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class ResponsePaymentListError
    {
        public string status { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public int TotalRecords { get; set; }
        public int TotalSuccessRecords { get; set; }
        public int TotalfailRecords { get; set; }
        public List<ResponsePaymentList> detail { get; set; }
    }
}