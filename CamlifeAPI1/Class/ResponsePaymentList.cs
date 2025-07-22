using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CamlifeAPI1.Class
{
    public class ResponsePaymentList
    {
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string PaymentReferenceNo { get; set; }
        public string TransactionType { get; set; }
        public string InsuranceApplicationId { get; set; }
        public string ClientNameENG { get; set; }
        public string Currency { get; set; }
        public string Premium { get; set; }
        public string PaymentDate { get; set; }
        public string ErrorMessage { get; set; }
    }
}