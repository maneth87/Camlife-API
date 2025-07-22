using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class bl_referral
    {
        public bl_referral() { }
        public int No { get; set; }
        public string ReferralId { get; set; }
        public string ChannelItemID { get; set; }
        public string ChannelLocationId { get; set; }
        public string ReferralStaffId { get; set; }
        public string ReferralStaffName { get; set; }
        public string ReferralStaffPosition { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Remarks { get; set; }
    }

