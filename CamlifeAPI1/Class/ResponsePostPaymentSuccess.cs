using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class ResponsePostPaymentSuccess
    {
     public string TransactionReferenceNumber { get; set; }
        public string PaymentCode { get; set; }
        public double ReceivedAmount { get; set; }
        public DateTime ReceivedDate { get; set; }
    }
}