using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class bl_PostPayment
    {
        public  bl_PostPayment() { }
        public string Id { get; set; }
        public string PaymentCode { get; set; }
    public string BillNo { get; set; }
        public string BillerId { get; set; }
        public string BillerName { get; set; }
        public double BillAmount { get; set; }
        public double FeeCharge { get; set; }
        public double TotalAmount { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionReferenceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string BankName { get; set; }
        public string CreatedBy { get;set; }
        public DateTime CreatedOn { get; set; } 
        public string SysRemarks { get; set; }
    }
